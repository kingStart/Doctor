﻿using Doctor.Model;
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
    public partial class Plan : Window
    {
        private SuspectedDisease suspectedDisease;

        public DiseaseDosageSchedule currentDiseaseDosageSchedule;
        public static Plan plan;


        private List<PaintName> _symptomsList = new List<PaintName>() { };//初始化加载的时候，用户病例中的症状列表

        public static int SQ1Status=0;

        public static int SQ2Status = 0;
        public static int SQ3Status = 0;
        public static int SQ4Status = 0;
        public static int SQ5Status = 0;
        public static int SQ6Status = 0;
        public static int SQ7Status = 0;
        public static int SQ8Status = 0;
        public static int SQ9Status = 0;

        public List<ProgramItem> proListItem;

        public Plan(SuspectedDisease _suspe, DiseaseDosageSchedule _DiseaseDosageSchedule)
        {
            InitializeComponent();
            this.suspectedDisease = _suspe;

            plan = this;

            currentDiseaseDosageSchedule = _DiseaseDosageSchedule;

            //WSocketClient.ShutDownOtherWindow("Plan");
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

            App._suspensionWindow._defultWindow = this;
            //加载疑似病例模块
            if (suspectedDisease != null && suspectedDisease.wmDiseaseDetailSocketParams != null)
            {
                
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

                    if (string.IsNullOrEmpty(wmDIs.icd10))
                    {
                        wmDIs.IsShowIcd = "Visible";
                    }
                    else
                    {
                        wmDIs.IsShowIcd = "Hidden";
                    }


                }
                this.YsListDb.ItemsSource = suspectedDisease.wmDiseaseDetailSocketParams;

                string icd = suspectedDisease.wmDiseaseDetailSocketParams.FirstOrDefault().icd10;



                if (currentDiseaseDosageSchedule == null)
                {
                    currentDiseaseDosageSchedule = WebApiService.GetPlanList(icd, suspectedDisease.patient.outpatientId);
                }
                //加载疾病知识库
               
                LinChuang.Text = currentDiseaseDosageSchedule.clinicalManifestation;
                ZhenDuan.Text = currentDiseaseDosageSchedule.differentialDiagnosis;
                GaiShu.Text = currentDiseaseDosageSchedule.diseasesummary;
                JianCha.Text = currentDiseaseDosageSchedule.laboratoryExamination;
                YuFang.Text = currentDiseaseDosageSchedule.prophylacticPrognosis;
                ZhiLiao.Text = currentDiseaseDosageSchedule.treatmentPlan;
                YuanZe.Text = currentDiseaseDosageSchedule.treatmentPrinciple;
                LiuXing.Text = currentDiseaseDosageSchedule.epidemiology;
                Changjian.Text = currentDiseaseDosageSchedule.commonSymptoms;

                //绑定一般治疗方法
                proListItem = currentDiseaseDosageSchedule.drugProgramAll.Take(4).ToList();
                ProgramMotherList.ItemsSource = proListItem;

                List<ProgramItem> proListItem1 = currentDiseaseDosageSchedule.drugProgramAll.Take(4).FirstOrDefault().childItem;
                //绑定药物治疗
                ItemProgramMotherList.ItemsSource = currentDiseaseDosageSchedule.drugProgramAll.Take(4).FirstOrDefault().childItem;

                //加载检查方案
                disCheckItem.ItemsSource = currentDiseaseDosageSchedule.diseaseCheckItem;

            }




            //加载症状
            if (!string.IsNullOrEmpty(suspectedDisease.patient.symptom)&& suspectedDisease.patient.symptom.Contains(","))
            {
                List<string> pList = suspectedDisease.patient.symptom.Split(new[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var u in pList)
                {
                    PaintName p = new PaintName();
                    p.Name = u;
                    p.IsSelf = true;
                    _symptomsList.Add(p);
                }
            }

            var newList = new List<PaintName>() { };
            newList.AddRange(_symptomsList);

            //疑似病例中的症状列表
            if (!string.IsNullOrEmpty(currentDiseaseDosageSchedule.commonSymptoms))
            {
                var list = currentDiseaseDosageSchedule.commonSymptoms.Split(new[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (var item in list)
                {
                    var exitItem = _symptomsList.Where(p => p.Name == item).FirstOrDefault();
                    if (exitItem == null)
                    {
                        PaintName p = new PaintName();
                        p.Name = item;
                        p.IsSelf = false;
                        newList.Add(p);
                    }
                }

            }


            painetDb.ItemsSource = newList;





        }
        /// <summary>
        /// 添加疾病
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDiseaseBtnSave_Click(object sender, RoutedEventArgs e)
        {


            //加载症状
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(suspectedDisease.patient.symptom) && suspectedDisease.patient.symptom.Contains(","))
            {
                list = suspectedDisease.patient.symptom.Split(new[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                
            }


            Search dlg = new Search(suspectedDisease.patient, list);
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dlg.Show();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var stackpanel = sender as StackPanel;
            string icd = stackpanel.Tag.ToString();

            //加载疾病知识库
            currentDiseaseDosageSchedule =  WebApiService.GetPlanList(icd, suspectedDisease.patient.outpatientId);
            LinChuang.Text = currentDiseaseDosageSchedule.clinicalManifestation;
            ZhenDuan.Text = currentDiseaseDosageSchedule.differentialDiagnosis;
            GaiShu.Text = currentDiseaseDosageSchedule.diseasesummary;
            JianCha.Text = currentDiseaseDosageSchedule.laboratoryExamination;
            YuFang.Text = currentDiseaseDosageSchedule.prophylacticPrognosis;
            ZhiLiao.Text = currentDiseaseDosageSchedule.treatmentPlan;
            YuanZe.Text = currentDiseaseDosageSchedule.treatmentPrinciple;
            LiuXing.Text = currentDiseaseDosageSchedule.epidemiology;
            Changjian.Text = currentDiseaseDosageSchedule.commonSymptoms;

            //绑定一般治疗方法
            proListItem = currentDiseaseDosageSchedule.drugProgramAll.Take(4).ToList();
            ProgramMotherList.ItemsSource = proListItem;

            List<ProgramItem> proListItem1= currentDiseaseDosageSchedule.drugProgramAll.Take(4).FirstOrDefault().childItem;
            //绑定药物治疗
            ItemProgramMotherList.ItemsSource = currentDiseaseDosageSchedule.drugProgramAll.Take(4).FirstOrDefault().childItem;

            //加载检查方案

            disCheckItem.ItemsSource = currentDiseaseDosageSchedule.diseaseCheckItem;



            var newList = new List<PaintName>() { };
            newList.AddRange(_symptomsList);
            //疑似病例中的症状列表
            if (!string.IsNullOrEmpty(currentDiseaseDosageSchedule.commonSymptoms))
            {
                var list = currentDiseaseDosageSchedule.commonSymptoms.Split(new[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (var item in list)
                {
                    var exitItem = _symptomsList.Where(p => p.Name == item).FirstOrDefault();
                    if (exitItem == null)
                    {
                        PaintName p = new PaintName();
                        p.Name = item;
                        p.IsSelf = false;
                        newList.Add(p);
                    }
                }
            }
            painetDb.ItemsSource = newList;
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var stackpanel = sender as Label;
            string itemCode = stackpanel.Tag.ToString();
            ItemProgramMotherList.ItemsSource = proListItem.Where(p=>p.itemCode==itemCode).FirstOrDefault().childItem;


        }

        private void ButtonSQ1_Click(object sender, RoutedEventArgs e)
        {
            if (SQ1Status == 0)
            {
                SQ1Status = 1;
                this.ButtonSQ1.Template = this.FindResource("ZhanKai") as ControlTemplate;
                SQ1Panel.Visibility = Visibility.Hidden;
                SQ1Panel.Height = 0;
            }
            else
            {
                this.ButtonSQ1.Template = this.FindResource("ShouQi") as ControlTemplate;
                SQ1Panel.Visibility = Visibility.Visible;
                SQ1Status = 0;
                SQ1Panel.Height = 150;
            }
           
        }

        private void ButtonSQ2_Click(object sender, RoutedEventArgs e)
        {
            if (SQ2Status == 0)
            {
                SQ2Status = 1;
                this.ButtonSQ2.Template = this.FindResource("ZhanKai") as ControlTemplate;
                SQ2Panel.Visibility = Visibility.Hidden;
                SQ2Panel.Height = 0;
            }
            else
            {
                this.ButtonSQ2.Template = this.FindResource("ShouQi") as ControlTemplate;
                SQ2Panel.Visibility = Visibility.Visible;
                SQ2Status = 0;
                SQ2Panel.Height = 150;
            }
        }

        private void ButtonSQ3_Click(object sender, RoutedEventArgs e)
        {
            if (SQ3Status == 0)
            {
                SQ3Status = 1;
                this.ButtonSQ3.Template = this.FindResource("ZhanKai") as ControlTemplate;
                SQ3Panel.Visibility = Visibility.Hidden;
                SQ3Panel.Height = 0;
            }
            else
            {
                this.ButtonSQ3.Template = this.FindResource("ShouQi") as ControlTemplate;
                SQ3Panel.Visibility = Visibility.Visible;
                SQ3Status = 0;
                SQ3Panel.Height = 150;
            }
        }

        private void ButtonSQ4_Click(object sender, RoutedEventArgs e)
        {
            if (SQ4Status == 0)
            {
                SQ4Status = 1;
                this.ButtonSQ4.Template = this.FindResource("ZhanKai") as ControlTemplate;
                SQ4Panel.Visibility = Visibility.Hidden;
                SQ4Panel.Height = 0;
            }
            else
            {
                this.ButtonSQ4.Template = this.FindResource("ShouQi") as ControlTemplate;
                SQ4Panel.Visibility = Visibility.Visible;
                SQ4Status = 0;
                SQ4Panel.Height = 150;
            }
        }

        private void ButtonSQ5_Click(object sender, RoutedEventArgs e)
        {
            if (SQ5Status == 0)
            {
                SQ5Status = 1;
                this.ButtonSQ5.Template = this.FindResource("ZhanKai") as ControlTemplate;
                SQ5Panel.Visibility = Visibility.Hidden;
                SQ5Panel.Height = 0;
            }
            else
            {
                this.ButtonSQ5.Template = this.FindResource("ShouQi") as ControlTemplate;
                SQ5Panel.Visibility = Visibility.Visible;
                SQ5Status = 0;
                SQ5Panel.Height = 150;
            }
        }

        private void ButtonSQ6_Click(object sender, RoutedEventArgs e)
        {
            if (SQ6Status == 0)
            {
                SQ6Status = 1;
                this.ButtonSQ6.Template = this.FindResource("ZhanKai") as ControlTemplate;
                SQ6Panel.Visibility = Visibility.Hidden;
                SQ6Panel.Height = 0;
            }
            else
            {
                this.ButtonSQ6.Template = this.FindResource("ShouQi") as ControlTemplate;
                SQ6Panel.Visibility = Visibility.Visible;
                SQ6Status = 0;
                SQ6Panel.Height = 150;
            }
        }

        private void ButtonSQ7_Click(object sender, RoutedEventArgs e)
        {
            if (SQ7Status == 0)
            {
                SQ7Status = 1;
                this.ButtonSQ7.Template = this.FindResource("ZhanKai") as ControlTemplate;
                SQ7Panel.Visibility = Visibility.Hidden;
                SQ7Panel.Height = 0;
            }
            else
            {
                this.ButtonSQ7.Template = this.FindResource("ShouQi") as ControlTemplate;
                SQ7Panel.Visibility = Visibility.Visible;
                SQ7Status = 0;
                SQ7Panel.Height = 150;
            }

        }
        private void ButtonSQ8_Click(object sender, RoutedEventArgs e)
        {
            if (SQ8Status == 0)
            {
                SQ8Status = 1;
                this.ButtonSQ8.Template = this.FindResource("ZhanKai") as ControlTemplate;
                SQ8Panel.Visibility = Visibility.Hidden;
                SQ8Panel.Height = 0;
            }
            else
            {
                this.ButtonSQ8.Template = this.FindResource("ShouQi") as ControlTemplate;
                SQ8Panel.Visibility = Visibility.Visible;
                SQ8Status = 0;
                SQ8Panel.Height = 150;
            }

        }
        private void ButtonSQ9_Click(object sender, RoutedEventArgs e)
        {
            if (SQ9Status == 0)
            {
                SQ9Status = 1;
                this.ButtonSQ9.Template = this.FindResource("ZhanKai") as ControlTemplate;
                SQ9Panel.Visibility = Visibility.Hidden;
                SQ9Panel.Height = 0;
            }
            else
            {
                this.ButtonSQ9.Template = this.FindResource("ShouQi") as ControlTemplate;
                SQ9Panel.Visibility = Visibility.Visible;
                SQ9Status = 0;
                SQ9Panel.Height = 150;
            }

        }
        /// <summary>
        /// 药品说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn.Tag != null)
            {
                string drugID = btn.Tag.ToString();

                string outpatientId = suspectedDisease.patient.outpatientId;


                YpExplain dlg = new YpExplain(drugID, outpatientId);
                dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                dlg.Show();
            }
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DcPlan dlg = new DcPlan(currentDiseaseDosageSchedule);
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dlg.Show();
        }
    }

    public class PaintName {
        public string Name { get; set; }
        public bool IsSelf { get; set; }
    }
}
