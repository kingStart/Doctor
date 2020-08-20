using Doctor.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Doctor
{
    /// <summary>
    /// SuspensionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SuspensionWindow : Window
    {
        public Window _defultWindow;

        public SuspensionWindow()
        {
            InitializeComponent();
        
        }
        
        public SuspensionWindow(DefultWindow defultWindow)
        {
            _defultWindow = defultWindow;
            InitializeComponent();
        }


        private void RootWindow_Loaded(object sender, RoutedEventArgs e)
        {

            //右键菜单项
            ContextMenu aMenu = new ContextMenu();
            MenuItem openMenu = new MenuItem();
            openMenu.Header = "打开客户端";
            openMenu.Click += openMenu_Click;
            aMenu.Items.Add(openMenu);

            MenuItem closeMenu = new MenuItem();
            closeMenu.Header = "关闭悬浮球";
            closeMenu.Click += closeMenu_Click;
            aMenu.Items.Add(closeMenu);
            windowIcon.ContextMenu = aMenu;

        }
        private void openMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            _defultWindow.Show();
            _defultWindow.WindowState = WindowState.Normal;
            _defultWindow.ShowInTaskbar = true;
            _defultWindow.Topmost = true;
        }
        private void closeMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void RootWindow_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void RootWindow_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void RootWindow_Activated(object sender, EventArgs e)
        {

        }

        private void RootWindow_Deactivated(object sender, EventArgs e)
        {

        }

        private void RootWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void RootWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void RootWindow_Closed(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 双击唤醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RootWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            App._suspensionWindow._defultWindow.Show();
            App._suspensionWindow._defultWindow.WindowState = WindowState.Normal;
            App._suspensionWindow._defultWindow.Topmost = true;
            App._suspensionWindow._defultWindow.ShowInTaskbar = true;


            this.Hide();
        }

        private void windowIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            App._suspensionWindow._defultWindow.Show();
            App._suspensionWindow._defultWindow.WindowState = WindowState.Normal;
            App._suspensionWindow._defultWindow.Topmost = true;
            App._suspensionWindow._defultWindow.ShowInTaskbar = true;


            this.Hide();
        }
    }
}
