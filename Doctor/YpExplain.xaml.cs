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
    public partial class YpExplain : Window
    {
        public DrugKnowledageBase _drugKnowledageBase;
        public YpExplain(string drugid, string outpatientId)
        {
            InitializeComponent();

            _drugKnowledageBase= WebApiService.GetDrug(drugid, outpatientId);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //System.Environment.Exit(0);
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
            this.BieMing.Content = "别名："+ _drugKnowledageBase.drugName;
            this.JiXing.Content = "剂型：" + _drugKnowledageBase.drugFormulation;
            this.GuiGe.Content = "规格：" + _drugKnowledageBase.drugSpecification;
            this.XingZhuang.Content = "形状：" + _drugKnowledageBase.basicInformation.drugShape;
            this.ShiYing.Text = _drugKnowledageBase.drugAdaptation;
            this.BuLiang.Text = _drugKnowledageBase.drugUntowardEffect;
            this.ShuoMing.Text = _drugKnowledageBase.drugInstructions;
            this.Jinji.Text = _drugKnowledageBase.drugTaboo;
         
        }
    }
}
