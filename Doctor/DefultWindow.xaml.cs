using Doctor.Model;
using Doctor.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Doctor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DefultWindow : Window
    {
        private DoctorInfo _doctor;

        private SuspensionWindow _suspensionWindow;


        public static DefultWindow defultWindow;
        public DefultWindow(DoctorInfo doctor)
        {
            this._doctor = doctor;
            icon();
            InitializeComponent();

            defultWindow = this;
        }

        


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _doctor;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            this.Hide();
            if (_suspensionWindow == null)
            {
                _suspensionWindow = new SuspensionWindow(this);
            }
            _suspensionWindow.Left = Screen.PrimaryScreen.WorkingArea.Width / (CommonHelper.GetScale()) - 100;
            _suspensionWindow.Top = Screen.PrimaryScreen.WorkingArea.Height / (CommonHelper.GetScale()) - 100;
            _suspensionWindow.Show();
        }

        /// <summary>
        /// 窗体可拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }




        NotifyIcon notifyIcon;//右下角图标


        private Icon icon0 = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);



        private void icon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.BalloonTipText = "全科医生助手辅诊系统"; //设置程序启动时显示的文本
            notifyIcon.Text = "全科医生助手辅诊系统";//最小化到托盘时，鼠标点击时显示的文本
                                         //System.Drawing.Icon icon = new System.Drawing.Icon(iconPath0);//程序图标 
            notifyIcon.Icon = icon0;
            notifyIcon.Visible = true;
            //notifyIcon.Click += OnNotifyIconClick;
            notifyIcon.ShowBalloonTip(1000);


            //右键菜单项
            System.Windows.Forms.MenuItem shoiwWindowMenuItem = new System.Windows.Forms.MenuItem("显示客户端");
            System.Windows.Forms.MenuItem showSuspensionWindowMenuItem = new System.Windows.Forms.MenuItem("显示/隐藏悬浮球");
            System.Windows.Forms.MenuItem showVersionMenuItem = new System.Windows.Forms.MenuItem("查看版本及版本说明");
            System.Windows.Forms.MenuItem quitMenuItem = new System.Windows.Forms.MenuItem("退出");
            showSuspensionWindowMenuItem.Click += showSuspensionWindowMenuItem_Click;
            showVersionMenuItem.Click += showVersionMenuItem_Click;
            quitMenuItem.Click += quitMenuItem_Click;
            shoiwWindowMenuItem.Click += OnNotifyIconClick;
            System.Windows.Forms.MenuItem[] parentMenuitem = new System.Windows.Forms.MenuItem[] { shoiwWindowMenuItem, showSuspensionWindowMenuItem, showVersionMenuItem,quitMenuItem };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(parentMenuitem);

        }


        private void OnNotifyIconClick(object sender, EventArgs e)
        {
            this.Show();
            WindowState = WindowState.Normal;
            Topmost = true;
            ShowInTaskbar = true;
        }

        /// <summary>
        /// 查看版本及版本说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showVersionMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.MessageBox.Show("当前版本号：1.0", "信息");
        }


        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
            this.Close();
        }


        /// <summary>
        /// 显示/隐藏悬浮球
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showSuspensionWindowMenuItem_Click(object sender, EventArgs e)
        {
            if(_suspensionWindow.Visibility == Visibility.Visible)
            {
                _suspensionWindow.Hide();
            }
            else
            {
                _suspensionWindow.Show();
            }

        }

    }
}
