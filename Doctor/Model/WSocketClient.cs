using Doctor.Model;
using Doctor.Service;
using Newtonsoft.Json.Linq;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WebSocket4Net;

namespace Doctor
{
    public class WSocketClient : IDisposable
    {

        public ZhuSu zShu;

        public DefultWindow dcInfo;

        public DefultNo deNo;

        public YsList yList;

        #region 向外传递数据事件
        public event Action<string> MessageReceived;
        #endregion

        WebSocket4Net.WebSocket _webSocket;
        /// <summary>
        /// 检查重连线程
        /// </summary>
        Thread _thread;
        bool _isRunning = true;
        /// <summary>
        /// WebSocket连接地址
        /// </summary>
        public string ServerPath { get; set; }

        public WSocketClient(string url)
        {
            ServerPath = url;
            this._webSocket = new WebSocket4Net.WebSocket(url);
            
            this._webSocket.Opened += WebSocket_Opened;
           this._webSocket.Error += WebSocket_Error;
            this._webSocket.Closed += WebSocket_Closed;
            this._webSocket.MessageReceived += WebSocket_MessageReceived;
        }

        #region "web socket "
        /// <summary>
        /// 连接方法
        /// <returns></returns>
        public bool Start()
        {
            bool result = true;
            try
            {
                this._webSocket.Open();

                this._isRunning = true;
                this._thread = new Thread(new ThreadStart(CheckConnection));
                this._thread.Start();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 消息收到事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
          
             
            LogHelper.Info(" Received:" + e.Message);

            JObject alive = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(e.Message);
            string page = alive["page"].ToString();
            if (!string.IsNullOrEmpty(page))
            {

            
                string datastr= alive["data"].ToString();
                //医生信息
                if (page == "doctor_info")
                {
                    DoctorInfo doc = Newtonsoft.Json.JsonConvert.DeserializeObject<DoctorInfo>(datastr);
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        

                         if (dcInfo != null)
                        {
                            dcInfo.Close();
                        }

                        dcInfo = new DefultWindow(doc);
                        dcInfo.WindowStartupLocation = WindowStartupLocation.Manual;
                        dcInfo.Left = System.Windows.SystemParameters.PrimaryScreenWidth - 380;
                        dcInfo.Top = System.Windows.SystemParameters.PrimaryScreenHeight - 630;
                        dcInfo.Topmost = true;
                        dcInfo.Show();

                        App._suspensionWindow._defultWindow = dcInfo;
                    }));

                }
                else if (page == "patient_info")
                {
                    PatientInfo paient = Newtonsoft.Json.JsonConvert.DeserializeObject<PatientInfo>(datastr);
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        if (zShu != null)
                        {
                            zShu.Close();
                        }



                        zShu = new ZhuSu(paient);
                        zShu.WindowStartupLocation = WindowStartupLocation.Manual;
                        zShu.Left = System.Windows.SystemParameters.PrimaryScreenWidth - 380;
                        zShu.Top = System.Windows.SystemParameters.PrimaryScreenHeight - 630;
                        zShu.Topmost = true;
                        zShu.Show();
                        App._suspensionWindow._defultWindow = zShu;
                    }));
                }
                else if (page == "disease_list")
                {
                    SuspectedDisease paient = Newtonsoft.Json.JsonConvert.DeserializeObject<SuspectedDisease>(datastr);
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        if (paient.wmDiseaseDetailSocketParams != null && paient.wmDiseaseDetailSocketParams.Count() > 0)
                        {
                            //var dlg = new MainWindow(paient.patient);
                            //dlg.Show();
                            if (yList != null)
                            {
                                yList.Close();
                            }

                            yList = new YsList(paient);
                            yList.WindowStartupLocation = WindowStartupLocation.Manual;
                            yList.Left = System.Windows.SystemParameters.PrimaryScreenWidth - 380;
                            yList.Top = System.Windows.SystemParameters.PrimaryScreenHeight - 630;
                            yList.Topmost = true;
                            yList.Show();
                            App._suspensionWindow._defultWindow = yList;
                        }
                        else
                        {
                            if (deNo != null)
                            {
                                deNo.Close();
                            }

                            deNo = new DefultNo(paient.patient);
                            deNo.WindowStartupLocation = WindowStartupLocation.Manual;
                            deNo.Left = System.Windows.SystemParameters.PrimaryScreenWidth - 380;
                            deNo.Top = System.Windows.SystemParameters.PrimaryScreenHeight - 630;
                            deNo.Topmost = true;
                            deNo.Show();
                            App._suspensionWindow._defultWindow = deNo;
                        }
                    }));
                }
                else
                {
                    SuspectedDisease paient = Newtonsoft.Json.JsonConvert.DeserializeObject<SuspectedDisease>(datastr);
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        if (deNo != null)
                        {
                            deNo.Close();
                        }

                        deNo = new DefultNo(paient.patient);
                        deNo.WindowStartupLocation = WindowStartupLocation.Manual;
                        deNo.Left = System.Windows.SystemParameters.PrimaryScreenWidth - 380;
                        deNo.Top = System.Windows.SystemParameters.PrimaryScreenHeight - 630;
                        deNo.Topmost = true;
                        deNo.Show();
                        App._suspensionWindow._defultWindow = deNo;

                    }));
                }
            }
            //MessageReceived?.Invoke(e.Message);
        }
        /// <summary>
        /// Socket关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebSocket_Closed(object sender, EventArgs e)
        {
            LogHelper.Info("websocket_Closed");
        }
        /// <summary>
        /// Socket报错事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebSocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            LogHelper.Info("websocket_Error:"+e.Exception.ToString() );
        }
        /// <summary>
        /// Socket打开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebSocket_Opened(object sender, EventArgs e)
        {
            LogHelper.Info(" websocket_Opened");
        }
        /// <summary>
        /// 检查重连线程
        /// </summary>
        private void CheckConnection()
        {
            do
            {
                try
                {
                    if (this._webSocket.State != WebSocket4Net.WebSocketState.Open && this._webSocket.State != WebSocket4Net.WebSocketState.Connecting)
                    {
                        LogHelper.Info(" Reconnect websocket WebSocketState:" + this._webSocket.State);
                        this._webSocket.Close();
                        this._webSocket.Open();
                        Console.WriteLine("正在重连");
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex.ToString());
                }
                System.Threading.Thread.Sleep(5000);
            } while (this._isRunning);
        }
        #endregion

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="Message"></param>
        public void SendMessage(string Message)
        {
            Task.Factory.StartNew(() =>
            {
                if (_webSocket != null && _webSocket.State == WebSocket4Net.WebSocketState.Open)
                {
                    this._webSocket.Send(Message);
                }
            });
        }

        public void Dispose()
        {
            this._isRunning = false;
            try
            {
                _thread.Abort();
            }
            catch
            {

            }
            this._webSocket.Close();
            this._webSocket.Dispose();
            this._webSocket = null;
        }


        public static void ShutDownOtherWindow( string Name)
        {
            if (Name != "DefultWindow")
            {
               
                if (DefultWindow.defultWindow != null)
                {
                    DefultWindow.defultWindow.Close();
                }
            }
            if (Name != "DefultNo")
            {
                if (DefultNo.defultNo != null)
                {
                    DefultNo.defultNo.Close();
                }
            }
            if (Name != "ZhuSu")
            {
                if (ZhuSu.zhuSu != null)
                {
                    ZhuSu.zhuSu.Close();
                }
               
            }
            if (Name != "YsList")
            {
                if (YsList.ysList != null)
                {
                    YsList.ysList.Close();
                }
               
            }
            
           
        }
    }
}
