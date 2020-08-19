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

        public SuspensionWindow _suspensionWindow;


        public static DefultWindow defultWindow;
        public DefultWindow(DoctorInfo doctor)
        {
            this._doctor = doctor;
            //icon();
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






        

    }
}
