using Doctor.Model;
using Doctor.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfControls.Editors;

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

        public DiseaseDosageSchedule currentDiseaseDosageSchedule;
     
        private PatientInfo _patieneInfo;
        public static MainWindow mainWindow;

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
        }

        public MainWindow(PatientInfo patientInfo)
        {
            _patieneInfo = patientInfo;
            InitializeComponent();
            mainWindow = this;
            App._suspensionWindow._defultWindow?.Close();
            App._suspensionWindow._defultWindow = this;

            //WSocketClient.ShutDownOtherWindow("MainWindow");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App._suspensionWindow._defultWindow = this;

            string sex = _patieneInfo.sexCode == "1" ? "男" : "女";
            this.PatientLabel.Content = "「 " + _patieneInfo.patientName + "   " + sex + "   " + _patieneInfo.age + " 」";



            //查询患者体征数据
            var healthData = WebApiService.QueryHealthData(_patieneInfo);
            if (healthData != null)
            {
                this.DataContext = healthData;
            }



            symptomListBox.ItemsSource = _symptomResultList;
            symptomResultListBox.ItemsSource = _symptomResultList2;


            acSearchTextBox.ValueSelected += AcSearchTextBox_ValueSelected;

        }

        private void AcSearchTextBox_ValueSelected(object sender, RoutedEventArgs e)
        {
            var t1 = sender as AutoCompleteTextBox;
            var symptom = t1.SelectedItem as Symptom;
            if (symptom != null)
            {
                t1.Editor.Text = "";
                t1.SelectedItem = null;

                if (_symptomResultList.Where(p => p.symptom == symptom.symptom).FirstOrDefault() == null)
                {
                    _symptomResultList.Add(symptom);
                    UpdateList();
                }
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

            UpdateList();
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

            if (_symptomResultList.Where(p => p.symptom == item.symptom).FirstOrDefault() == null)
            {
                _symptomResultList.Add(item);
                UpdateList();
            }

        }

        private void UpdateList()
        {
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                var sn = string.Empty;
                //发出请求，查询
                foreach (var s in _symptomResultList)
                {
                    sn = s.symptom + ",";
                }

                if (string.IsNullOrEmpty(sn))
                {
                    _diagnosisDiseaseList.Clear();

                    _symptomResultList2.Clear();

                    this.YsListDb.ItemsSource = _diagnosisDiseaseList;
                    this.totalCountLabel.Content = "共计：0" ;



                    return;
                }

                var list = WebApiService.QueryDiseaseBySymptom(sn, _patieneInfo);


                var suspectedDisease = list.diagnosisDisease;
                var sList = list.concomitant;


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
                            wmDIs.degreeWei = "Collapsed";
                        }
                        if (wmDIs.degree.Contains("急"))
                        {
                            wmDIs.degreeJi = "Visible";
                        }
                        else
                        {
                            wmDIs.degreeJi = "Collapsed";
                        }
                        if (wmDIs.degree.Contains("重"))
                        {
                            wmDIs.degreeZ = "Visible";
                        }
                        else
                        {
                            wmDIs.degreeZ = "Collapsed";
                        }

                        if (string.IsNullOrEmpty(wmDIs.icd10))
                        {
                            wmDIs.IsShowIcd = "Visible";
                        }
                        else
                        {
                            wmDIs.IsShowIcd = "Collapsed";
                        }

                        wmDIs.diseaseMatching = string.IsNullOrEmpty(wmDIs.diseaseMatching)?"--" : wmDIs.diseaseMatching + "%";
                        _diagnosisDiseaseList.Add(wmDIs);
                    }
                    if (_diagnosisDiseaseList.Count > 4)
                    {
                        this.YsListDb.ItemsSource = _diagnosisDiseaseList.Take(4);
                    }
                    else
                    {
                        this.YsListDb.ItemsSource = _diagnosisDiseaseList;
                    }

                    this.totalCountLabel.Content = "共计：" + suspectedDisease.Count();
                }

                _symptomResultList2.Clear();
                if(sList!=null && sList.Any())
                {
                    foreach (var item in sList)
                    {
                        _symptomResultList2.Add(item);
                    }
                }
            }));

            
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
            if(_diagnosisDiseaseList.Any() && _diagnosisDiseaseList.Count > 4)
            {
                this.YsListDb.ItemsSource = _diagnosisDiseaseList;
            }
            else
            {
                MessageBox.Show("没有更多的疑似病例了", "提示");
            }

            //var symptomString = "";


            //if (!_diagnosisDiseaseList.Any())
            //{
            //    MessageBox.Show("请输入主诉症状进行查询", "提示");
            //}




            //if (_symptomResultList != null && _symptomResultList.Any())
            //{
            //    foreach (var s in _symptomResultList)
            //    {
            //        symptomString += s.symptom + ",";
            //    }

            //    var item = new SuspectedDisease();
            //    item.patient = _patieneInfo;
            //    item.patient.symptom = symptomString;


            //    item.wmDiseaseDetailSocketParams = _diagnosisDiseaseList.ToList();

            //    Plan dlg = new Plan(item,null);
            //    dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //    dlg.Show();

            //    this.Close();

            //}
            //else
            //{
            //    MessageBox.Show("请输入主诉症状进行查询", "提示");
            //}
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var symptomString = "";

            foreach (var s in _symptomResultList)
            {
                symptomString += s.symptom + ",";
            }
            var item = new SuspectedDisease();
            item.patient = _patieneInfo;
            item.patient.symptom = symptomString;


            item.wmDiseaseDetailSocketParams = _diagnosisDiseaseList.ToList();
            var stackpanel = sender as StackPanel;
            string icd = stackpanel.Tag.ToString();
            currentDiseaseDosageSchedule = WebApiService.GetPlanList(icd, item.patient.outpatientId);
            Plan dlg = new Plan(item, currentDiseaseDosageSchedule);
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dlg.Show();
            this.Close();
        }


        /// <summary>
        /// 拖动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleStackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
