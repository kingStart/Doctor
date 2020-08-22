using Doctor.Model;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Doctor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ZhuSu : Window
    {
        public static PatientInfo pInfo;

        public static ZhuSu zhuSu;

        public ZhuSu(PatientInfo _pInfo)
        {
            pInfo = _pInfo;
            InitializeComponent();
            zhuSu = this;
            App._suspensionWindow._defultWindow?.Close();
            App._suspensionWindow._defultWindow = this;

            //WSocketClient.ShutDownOtherWindow("ZhuSu");
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
          
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();

            App._suspensionWindow.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width / (CommonHelper.GetScale()) - 100;
            App._suspensionWindow.Top = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height / (CommonHelper.GetScale()) - 100;
            App._suspensionWindow.Show();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sex = pInfo.sexCode == "1" ? "男" : "女";
            this.MainLabel.Content = "「 "+ pInfo.patientName+ "   "+ sex + "   "+ pInfo.age+ " 」";
        }
    }
}
