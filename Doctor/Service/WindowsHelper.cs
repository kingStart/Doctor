using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using HWND = System.IntPtr;

namespace Doctor.Service
{
    public class WindowsHelper
    {

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        const UInt32 SWP_NOZORDER = 0x0004;
        const int WM_ACTIVATEAPP = 0x001C;
        const int WM_ACTIVATE = 0x0006;
        const int WM_SETFOCUS = 0x0007;
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        const int WM_WINDOWPOSCHANGING = 0x0046;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);


        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public static IntPtr GetHandleWindow(string title)
        {
            return FindWindow(null, title);
        }

        

        private delegate bool EnumWindowsProc(HWND hWnd, int lParam);

        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(HWND hWnd);

        [DllImport("USER32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();

        //[DllImport("USER32.DLL")]
        //private static extern IntPtr GetDesktopWindow();

        [DllImport("dwmapi.dll")]
        static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out bool pvAttribute, int cbAttribute);
        [Flags]
        public enum DwmWindowAttribute : uint
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_LAST
        }
        public static IDictionary<HWND, string> GetOpeningWindows()
        {
            HWND shellWindow = GetShellWindow();
            Dictionary<HWND, string> windows = new Dictionary<HWND, string>();

            EnumWindows(delegate (HWND hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;
                if (IsIconic(hWnd)) return true;

                bool isCloacked;
                DwmGetWindowAttribute(hWnd, (int)DwmWindowAttribute.DWMWA_CLOAKED, out isCloacked, Marshal.SizeOf(typeof(bool)));
                if (isCloacked)
                    return true;


                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);
                windows[hWnd] = builder.ToString();

                return true;

            }, 0);

            return windows;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner

            public override string ToString()
            {
                return $"Left:{Left}, Top:{Top}, Right:{Right}, Bottom:{Bottom}";
            }
        }


        public static Rectangle GetWinRectangleByName(string winName)
        {
            if (string.IsNullOrEmpty(winName))
                return Rectangle.Empty;
            var winHandle = GetHandleWindow(winName);
            return GetWinRectangleByHandle(winHandle);
        }

        public static Rectangle GetWinRectangleByHandle(HWND hWnd)
        {
            if (hWnd == HWND.Zero) return Rectangle.Empty;
            RECT rect;
            var suc = GetWindowRect(new HandleRef(null, hWnd), out rect);
            //Debug.WriteLine($"{winName} RECT: {rect.ToString()}");
            Rectangle rectangle = new Rectangle();
            rectangle.X = rect.Left;
            rectangle.Y = rect.Top;
            rectangle.Width = rect.Right - rect.Left;
            rectangle.Height = rect.Bottom - rect.Top;
            //Debug.WriteLine($"{winName} Rectangle: {rectangle.ToString()}");
            return rectangle;
        }
    }
}
