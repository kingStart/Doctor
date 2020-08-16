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

        private ObservableCollection<string> _symptomList = new ObservableCollection<string>() { };//症状搜索集合
        private ObservableCollection<Symptom> _symptomResultList = new ObservableCollection<Symptom>() { };//症状搜索结果集合


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            searchText.OnSearch += SearchText_OnSearch;

            _symptomList.Add("感冒");
            _symptomList.Add("发烧");
            _symptomList.Add("感冒感冒感冒");
            _symptomList.Add("发烧");
            _symptomList.Add("瞌睡");
            _symptomList.Add("劳累");
            _symptomList.Add("劳累");
            _symptomList.Add("感冒");
            _symptomList.Add("发烧");
            _symptomList.Add("感冒");
            _symptomList.Add("感冒感冒感冒");
            _symptomList.Add("感冒");
            _symptomList.Add("发烧");
            _symptomList.Add("感冒感冒感冒");
            _symptomList.Add("发烧");
            _symptomList.Add("感冒");
            _symptomList.Add("感冒感冒感冒");
            _symptomList.Add("感冒");
            _symptomList.Add("发烧");

            symptomListBox.ItemsSource = _symptomList;
            //DataContext = this;


            //查询患者体征数据
            var healthData = WebApiService.QueryHealthData();
            if (healthData != null)
            {
                this.DataContext = healthData;
                //healthData = new HealthData();
                //healthData.bloodGlucose = "100";
            }
            


            symptomResultListBox.ItemsSource = _symptomResultList;


           

        }

        private void SearchText_OnSearch(object sender, SearchEventArgs e)
        {
            //搜索症状
            var text = searchText.GetText();
            
            if (string.IsNullOrEmpty(text))
            {
                return;
            }


            var list = WebApiService.QuerySymptomList(text);

            //var re = WebApiService.QueryDiseaseBySymptom();
            #region
            //加载疑似病例模块
            //if (suspectedDisease != null && suspectedDisease.wmDiseaseDetailSocketParams != null)
            //{

            //    foreach (var wmDIs in suspectedDisease.wmDiseaseDetailSocketParams)
            //    {


            //        wmDIs.diseaseMatching = wmDIs.diseaseMatching + "%";
            //    }
            //    this.YsListDb.ItemsSource = suspectedDisease.wmDiseaseDetailSocketParams;
            //}
            #endregion

            if (list.Any())
            {
                if (!_symptomList.Contains(text))
                {
                    _symptomList.Add(text);
                    foreach(var item in list)
                    {
                        _symptomResultList.Add(item);
                    }
                }
                else
                {
                    //已经搜索该症状
                }

            }
            else
            {
                MessageBox.Show("未查询到相关搜索结果");
            }

        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

        private void DeleteSymptomButton_Click(object sender, RoutedEventArgs e)
        {
            //var index = symptomListBox.SelectedIndex;

            Button btn = sender as Button;
            var obj = _symptomList.First(item => item == btn.Tag.ToString());
            if (!string.IsNullOrEmpty(obj))
            {
                _symptomList.Remove(obj);
            }
        }

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
    }
}
