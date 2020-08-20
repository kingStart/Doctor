using Doctor.Model;
using Doctor.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class MainWindow : Window
    {

        /*
         1. 输入症状后搜索 症状集合及分层分析的数据  请求“api/assistant/wm/querySymptomList

         2. 点击主诉症状的某个症状，调用搜索症状接口（用响应数据去更新症状分层分析的数据及疑似疾病列表（右侧）的数据）。 
                调用“api/assistant/wm/queryDiseaseBySymptom

         3. 症装分层分析里的数据点击，把点击数据累加到原来搜索症状数据（symptom)中，重新进行第2步骤
         

            1 =>3 =>2
            queryDiseaseBySymptom  这个接口请求的结果，显示在【症状分析层】和 【疑似疾病】中
            【症状分析层】中的症状点击后结果也是 显示在【症状分析层】和 【疑似疾病】中
         */

        //private ObservableCollection<string> _symptomList = new ObservableCollection<string>() { };//症状搜索集合
        private ObservableCollection<Symptom> _symptomResultList = new ObservableCollection<Symptom>() { };//症状搜索结果集合 主诉症状
         
        private ObservableCollection<Symptom> _symptomResultList2 = new ObservableCollection<Symptom>() { };//症状搜索结果集合  分析层症状
        private ObservableCollection<SuspectedDiseaseDetail> _diagnosisDiseaseList = new ObservableCollection<SuspectedDiseaseDetail>() { };//疑似疾病



        private PatientInfo _patieneInfo;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(PatientInfo patientInfo)
        {
            _patieneInfo = patientInfo;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App._suspensionWindow._defultWindow = this;

            string sex = _patieneInfo.sexCode == "1" ? "男" : "女";
            this.PatientLabel.Content = "「 " + _patieneInfo.patientName + "   " + sex + "   " + _patieneInfo.age + " 」";


            searchText.OnSearch += SearchText_OnSearch;

            //_symptomList.Add("感冒");
            //_symptomList.Add("发烧");
            //_symptomList.Add("感冒感冒感冒");
            //_symptomList.Add("发烧");
            //_symptomList.Add("瞌睡");
            //_symptomList.Add("劳累");
            //_symptomList.Add("劳累");
            //_symptomList.Add("感冒");
            //_symptomList.Add("发烧");
            //_symptomList.Add("感冒");
            //_symptomList.Add("感冒感冒感冒");
            //_symptomList.Add("感冒");
            //_symptomList.Add("发烧");
            //_symptomList.Add("感冒感冒感冒");
            //_symptomList.Add("发烧");
            //_symptomList.Add("感冒");
            //_symptomList.Add("感冒感冒感冒");
            //_symptomList.Add("感冒");
            //_symptomList.Add("发烧");

            //symptomListBox.ItemsSource = _symptomList;
            //DataContext = this;


            //查询患者体征数据
            var healthData = WebApiService.QueryHealthData(_patieneInfo);
            if (healthData != null)
            {
                this.DataContext = healthData;
                //healthData = new HealthData();
                //healthData.bloodGlucose = "100";
            }



            symptomListBox.ItemsSource = _symptomResultList;
            symptomResultListBox.ItemsSource = _symptomResultList2;




        }

        private void SearchText_OnSearch(object sender, SearchEventArgs e)
        {
            //搜索症状
            var text = searchText.GetText();
            
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            var list = WebApiService.QuerySymptomList(text, _patieneInfo);




            if (list.Any())
            {
                foreach (var item in list)
                {
                    _symptomResultList.Add(item);
                }
            }
            else
            {
                MessageBox.Show("未查询到相关搜索结果","提示");
            }

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

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

        /// <summary>
        /// 点击主诉症状的某一项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchSymptomDisease_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var label = sender as Label;
            var sn = label.Content.ToString();

            var re = WebApiService.QueryDiseaseBySymptom(sn,_patieneInfo);


            var suspectedDisease = re.diagnosisDisease;
            var sList = re.concomitant;

            _diagnosisDiseaseList.Clear();

            //加载疑似病例模块
            if (suspectedDisease != null && suspectedDisease.Any())
            {

                foreach (var wmDIs in suspectedDisease)
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

                    wmDIs.diseaseMatching = wmDIs.diseaseMatching + "%";
                    _diagnosisDiseaseList.Add(wmDIs);
                }
                this.YsListDb.ItemsSource = suspectedDisease;
                this.totalCountLabel.Content = "共计：" + suspectedDisease.Count();
            }

            if(sList != null && sList.Any())
            {
                _symptomResultList2.Clear();
                foreach (var item in sList)
                {
                    _symptomResultList2.Add(item);
                }
            }

        }

        /// <summary>
        /// 删除主诉症状的某一项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteSymptomButton_Click(object sender, RoutedEventArgs e)
        {
            //var index = symptomListBox.SelectedIndex;

            Button btn = sender as Button;
            var str = btn.Tag.ToString();

            var obj = _symptomResultList.FirstOrDefault(item => item.symptom == str);
            if (obj!=null)
            {
                _symptomResultList.Remove(obj);
            }
        }




        /// <summary>
        /// 点击症状分析层的某一项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SymptomResultButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            var tag = btn.Tag as string;
            if (tag == "true")
            {
                btn.Tag = "false";
                btn.SetValue(Button.StyleProperty, Application.Current.Resources["WhiteButton"]);
            }
            else
            {
                btn.Tag = "true";
                btn.SetValue(Button.StyleProperty, Application.Current.Resources["GreenButton"]);
            }

            //添加到主诉症状
            var item = new Symptom();
            item.symptom = btn.Content.ToString();

            _symptomResultList.Add(item);

        }

        private void SymptomCheckBox_Click(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;

            if (check.IsChecked.Value)
            {
                check.IsChecked = false;
            }
            else
            {
                check.IsChecked = true;
            }
        }

        /// <summary>
        /// 点击查看更多
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {
            var symptomString = "";


            if (!_diagnosisDiseaseList.Any())
            {
                MessageBox.Show("请输入主诉症状进行查询", "提示");
            }




            if (_symptomResultList != null && _symptomResultList.Any())
            {
                foreach (var s in _symptomResultList)
                {
                    symptomString += s.symptom + ",";
                }

                var item = new SuspectedDisease();
                item.patient = _patieneInfo;
                item.patient.symptom = symptomString;


                item.wmDiseaseDetailSocketParams = _diagnosisDiseaseList.ToList();

                Plan dlg = new Plan(item);
                dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                dlg.Show();

                this.Close();

            }
            else
            {
                MessageBox.Show("请输入主诉症状进行查询", "提示");
            }
        }
    }
}
