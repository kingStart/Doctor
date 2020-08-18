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
    public partial class DcPlan : Window
    {
        public DiseaseDosageSchedule _DiseaseDosageSchedule;
        public DcPlan(DiseaseDosageSchedule diseaseDosageSchedule)
        {
            InitializeComponent();
            _DiseaseDosageSchedule = diseaseDosageSchedule;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
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
            List<ProgramItem> drugProgramAll = _DiseaseDosageSchedule.drugProgramAll;
            //绑定一般治疗方法
            ProgramMotherList.ItemsSource = drugProgramAll;

            foreach (var drug in drugProgramAll)
            {
                drug.itemChidNames = " ";
                if (drug.childItem != null && drug.childItem.Count() > 0)
                {
                    foreach (var item in drug.childItem)
                    {
                        drug.itemChidNames = drug.itemChidNames + item.itemName + " ";
                    }
                   
                }
            }

            ItemProgramMotherList.ItemsSource = drugProgramAll;
        }
    }
}
