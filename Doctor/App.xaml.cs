﻿using Doctor.Model;
using Doctor.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Doctor
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        // 这里的URL配置成你websocket服务端的地址就好了
        private static string url = ConfigurationManager.AppSettings["WebSocketUrl"];
        WSocketClient client = new WSocketClient(url);

        protected override void OnStartup(StartupEventArgs e)
        {
            //hook on error before app really starts
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            base.OnStartup(e);
            //用传过来的参数登陆
            var doctor = new DoctorInfo();
            doctor.doctorName = "稳献萤";
            doctor.doctorId = "34182587845910";
            doctor.orgCode = "341825001";
            doctor.areaCodeCount = "341825";
            doctor.sourceId = "mmednet_jqkj";
            doctor.doctorToken = "";
            doctor.doctorSex = "";

            doctor = WebApiService.LoginUser(doctor);

            var dlg = new DefultWindow(doctor);
            dlg.ShowDialog();


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
                        MessageBox.Show("程序已经运行！");
                        System.Environment.Exit(0);
                        return;
                    }
                }
            }
            client.Start();
            //System.Diagnostics.Process.Start("cmd.exe", "regsvr32 Ry4SCom.dll");
            MessageReceived();

        }
        /// <summary>
        /// 服务端返回的消息
        /// </summary>
        private void MessageReceived()
        {
            //注册消息接收事件，接收服务端发送的数据
            client.MessageReceived += (data) => {
                MessageBox.Show(data);
            };
        }

    }
}
