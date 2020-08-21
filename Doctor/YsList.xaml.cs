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
    public partial class YsList : Window
    {
        private SuspectedDisease suspectedDisease;
        public DiseaseDosageSchedule currentDiseaseDosageSchedule;
        public static YsList ysList;
        public YsList(SuspectedDisease _suspe)
        {
          
            InitializeComponent();
            this.suspectedDisease = _suspe;
            ysList = this;

            WSocketClient.ShutDownOtherWindow("YsList");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sex = suspectedDisease.patient.sexCode == "1" ? "男" : "女";
            this.MainLabel.Content = "「 " + suspectedDisease.patient.patientName + "   " + sex + "   " + suspectedDisease.patient.age + " 」";
            int _MainCount = 0;
            if (suspectedDisease != null && suspectedDisease.wmDiseaseDetailSocketParams != null)
            {
                _MainCount = suspectedDisease.wmDiseaseDetailSocketParams.Count();

                if (_MainCount < 5)
                {
                    this.More_Btn.Visibility = Visibility.Hidden;
                }
                this.MainCount.Content = "共计：" + _MainCount.ToString();
                foreach (var wmDIs in suspectedDisease.wmDiseaseDetailSocketParams)
                {
                    if (wmDIs.degree.Contains("危"))
                    {
                        wmDIs.degreeWei = "Visible";
                    }
                    else
                    {
                        wmDIs.degreeWei = "Hidden";
                    }
                    if (wmDIs.degree.Contains("急"))
                    {
                        wmDIs.degreeJi = "Visible";
                    }
                    else
                    {
                        wmDIs.degreeJi = "Hidden";
                    }
                    if (wmDIs.degree.Contains("重"))
                    {
                        wmDIs.degreeZ = "Visible";
                    }
                    else
                    {
                        wmDIs.degreeZ = "Hidden";
                    }


                    wmDIs.diseaseMatching = wmDIs.diseaseMatching+"%";


                    if (string.IsNullOrEmpty(wmDIs.icd10))
                    {
                        wmDIs.IsShowIcd = "Visible";
                    }
                    else
                    {
                        wmDIs.IsShowIcd = "Hidden";
                    }

                }

                if (suspectedDisease.wmDiseaseDetailSocketParams.Count > 4)
                {
                    this.YsListDb.ItemsSource = suspectedDisease.wmDiseaseDetailSocketParams.Take(4);
                }
                else
                {
                    this.YsListDb.ItemsSource = suspectedDisease.wmDiseaseDetailSocketParams;
                }

                
            }


        }
        /// <summary>
        /// 查看更多
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {
            this.YsListDb.ItemsSource = suspectedDisease.wmDiseaseDetailSocketParams;
        }


        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DButton_Click(object sender, RoutedEventArgs e)
        {
            Plan dlg = new Plan(suspectedDisease, null);
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dlg.Show();
            this.Close();
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var stackpanel = sender as StackPanel;
            string icd = stackpanel.Tag.ToString();
            currentDiseaseDosageSchedule = WebApiService.GetPlanList(icd, suspectedDisease.patient.outpatientId);
            Plan dlg = new Plan(suspectedDisease, currentDiseaseDosageSchedule);
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dlg.Show();
            this.Close();
        }
    }
}
