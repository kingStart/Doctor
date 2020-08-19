using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;

namespace Doctor.Service
{
    public static class CommonHelper
    {
        /// <summary>
        /// 根据视频的宽与高的比例，得到正确的视频显示大小，高度固定为refo
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="refo"></param>
        /// <returns></returns>
        public static int GetWidth(int width, int height, int refo)
        {
            var lwidth = Convert.ToDecimal(width * refo / height);
            return Convert.ToInt32(lwidth);
        }
        /// <summary>
        /// 根据视频的宽与高的比例，得到正确的视频显示大小，宽度固定为refo
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="refo"></param>
        /// <returns></returns>
        public static int GetHeight(int width, int height, int refo)
        {
            var lheight = Convert.ToDecimal(height * refo / width);
            return Convert.ToInt32(lheight);
        }


        

        public static float GetScale()
        {
            Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
            float DIP = graphics.DpiX;//以96为基准，可以计算出当前系统的桌面的缩放比例

            return DIP / 96;
        }


        /// <summary>
        /// 获得当前主屏幕的桌面分辨率的宽度
        /// </summary>
        /// <returns></returns>
        public static int GetPrimaryScreenWidth()
        {
            var deskSize = System.Windows.Forms.SystemInformation.PrimaryMonitorSize;

            return deskSize.Width;
        }

        /// <summary>
        /// 获得当前主屏幕的桌面分辨率的高度
        /// </summary>
        /// <returns></returns>
        public static int GetPrimaryScreenHeight()
        {
            var deskSize = System.Windows.Forms.SystemInformation.PrimaryMonitorSize;
            return deskSize.Height;

        }

        /// <summary>
        /// 根据字符串大小的宽x高 得到int类型的宽度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetWidth(string str)
        {
            var width = str.Replace(" ", "");
            try
            {
                width = width.Substring(0, width.IndexOf("x"));
                return Convert.ToInt32(width);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据字符串大小的宽x高 得到int类型的高度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetHeight(string str)
        {
            var height = str.Replace(" ", "");
            try
            {
                height = height.Substring(height.IndexOf("x") + 1);
                return Convert.ToInt32(height);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static void KillProc(string strProcName)
        {
            try
            {
                //精确进程名  用GetProcessesByName
                foreach (Process p in Process.GetProcessesByName(strProcName))
                {
                    if (!p.CloseMainWindow())
                    {
                        p.Kill();
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }


        /// <summary>
        /// 获取本地IP地址信息
        /// </summary>
        public static string GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }
    }
}
