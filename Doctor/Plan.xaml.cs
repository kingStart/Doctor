using Doctor.Model;
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
    public partial class Plan : Window
    {
        private SuspectedDisease suspectedDisease;
        public static Plan plan;
        public Plan(SuspectedDisease _suspe)
        {
            InitializeComponent();
            this.suspectedDisease = _suspe;

            plan = this;

            //WSocketClient.ShutDownOtherWindow("Plan");
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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
            //加载疑似病例模块
            if (suspectedDisease != null && suspectedDisease.wmDiseaseDetailSocketParams != null)
            {
                
                foreach (var wmDIs in suspectedDisease.wmDiseaseDetailSocketParams)
                {


                    wmDIs.diseaseMatching = wmDIs.diseaseMatching + "%";
                }
                this.YsListDb.ItemsSource = suspectedDisease.wmDiseaseDetailSocketParams;
            }



            //加载症状
            List<PaintName> painetList = new List<PaintName>();
            if (!string.IsNullOrEmpty(suspectedDisease.patient.symptom)&& suspectedDisease.patient.symptom.Contains(","))
            {
                List<string> pList = suspectedDisease.patient.symptom.Split(',').ToList();
                foreach (var u in pList)
                {
                    PaintName p = new PaintName();
                    p.Name = u;
                    painetList.Add(p);
                }

                painetDb.ItemsSource = painetList;
            }


        }
        /// <summary>
        /// 添加疾病
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            Search dlg = new Search();
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dlg.Show();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var stackpanel = sender as StackPanel;
            string icd = stackpanel.Tag.ToString();

        }
    }

    public class PaintName {
        public string Name { get; set; }
    }
}
