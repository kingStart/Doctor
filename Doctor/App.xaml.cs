using Doctor.Model;
using Doctor.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace Doctor
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : System.Windows.Application
    {

        public App()
        {
            log4net.Config.XmlConfigurator.Configure();
            this.Startup += new StartupEventHandler(Application_Startup);
        }

        // 这里的URL配置成你websocket服务端的地址就好了
        private static string url = ConfigurationManager.AppSettings["WebSocketUrl"];
        public static DoctorInfo doctor;
        public static PatientInfo _patieneInfo;


        public static SuspensionWindow _suspensionWindow;

        protected override void OnStartup(StartupEventArgs e)
        {

            //var n = new SuspensionWindow();
            //n.ShowDialog();
            //return;

            //hook on error before app really starts
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            doctor = new DoctorInfo();

            if (e.Args.Length > 0)
            {
                var str = e.Args[0];

                LogHelper.Info("初始启动参数--->"+ str);


                if (!string.IsNullOrEmpty(str))
                {
                    var paramsString = StringHelper.UnBase64String(str);
                    var paramsList = paramsString.Split(new Char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);


                    LogHelper.Info("解密后启动参数--->" + paramsString);

                    //用传过来的参数登陆
                    doctor.doctorName = paramsList[0];
                    doctor.doctorId = paramsList[1];
                    doctor.orgCode = paramsList[2];
                    doctor.areaCodeCount = paramsList[3];
                    doctor.sourceId = paramsList[4];

                }
            }            
            else
            {

                //LogHelper.Info("无参数启动--->");
                //System.Windows.MessageBox.Show("启动失败，无效的启动参数", "错误");
                //Environment.Exit(0);
                //return;

                //用传过来的参数登陆
                doctor.doctorName = "南宫医生";
                doctor.doctorId = "000010008";
                doctor.orgCode = "341825001";
                doctor.areaCodeCount = "341825";
                doctor.sourceId = "mmednet_jqkj";
            }
            doctor.doctorToken = "";
            doctor.doctorSex = "";






            doctor = WebApiService.LoginUser(doctor);
            string socketUrl = url + "/his/doctor/websocket/" + doctor.doctorId + "/" + doctor.doctorToken;

            WSocketClient client = new WSocketClient(socketUrl);

            client.Start();


            base.OnStartup(e);
            icon();

            _suspensionWindow = new SuspensionWindow();




            var dlg = new DefultWindow(doctor);
            dlg.WindowStartupLocation = WindowStartupLocation.Manual;
            dlg.Left = System.Windows.SystemParameters.PrimaryScreenWidth - 380;
            dlg.Top = System.Windows.SystemParameters.PrimaryScreenHeight - 630;
            dlg.Topmost = true;
            dlg.Show();


            _suspensionWindow._defultWindow = dlg;

        }
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //put your tracing or logging code here (I put a message box as an example)
            System.Windows.Forms.MessageBox.Show(e.ExceptionObject.ToString());
        }


        private void Application_Startup(object sender, StartupEventArgs e)
        {

            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (process.MainModule.FileName == current.MainModule.FileName)
                    {
                        System.Windows.MessageBox.Show("程序已经运行！");
                        System.Environment.Exit(0);
                        return;
                    }
                }
            }


        }
        /// <summary>
        /// 服务端返回的消息
        /// </summary>
        private void MessageReceived()
        {
            //注册消息接收事件，接收服务端发送的数据

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
            notifyIcon.Click += OnNotifyIconClick;
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
            System.Windows.Forms.MenuItem[] parentMenuitem = new System.Windows.Forms.MenuItem[] { shoiwWindowMenuItem, showSuspensionWindowMenuItem, showVersionMenuItem, quitMenuItem };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(parentMenuitem);

        }


        private void OnNotifyIconClick(object sender, EventArgs e)
        {
            App._suspensionWindow._defultWindow.Show();
            App._suspensionWindow._defultWindow.WindowState = WindowState.Normal;
            App._suspensionWindow._defultWindow.Topmost = true;
            App._suspensionWindow._defultWindow.ShowInTaskbar = true;
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
            
        }


        /// <summary>
        /// 显示/隐藏悬浮球
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showSuspensionWindowMenuItem_Click(object sender, EventArgs e)
        {
            if (_suspensionWindow.Visibility == Visibility.Visible)
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
