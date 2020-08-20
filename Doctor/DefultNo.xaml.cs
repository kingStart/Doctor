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
    public partial class DefultNo : Window
    {
        private PatientInfo pInfo;

        public static DefultNo defultNo;
        public DefultNo(PatientInfo _pInfo)
        {
            InitializeComponent();
            pInfo = _pInfo;
            defultNo = this;
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

            App._suspensionWindow.Left = Screen.PrimaryScreen.WorkingArea.Width / (CommonHelper.GetScale()) - 100;
            App._suspensionWindow.Top = Screen.PrimaryScreen.WorkingArea.Height / (CommonHelper.GetScale()) - 100;
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
            this.MainLabel.Content = "「 " + pInfo.patientName + "   " + sex + "   " + pInfo.age + " 」";
        }

        private void MainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new MainWindow(pInfo);
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dlg.Show();


            this.Close();
        }
    }
}
