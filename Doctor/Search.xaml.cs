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
    public partial class Search : Window
    {
        private ObservableCollection<string> _symptomList = new ObservableCollection<string>() { };
        private PatientInfo _patientInfo = new PatientInfo();

        private List<string> _painetList = new List<string>();//父级窗口带过来的已经选的症状


        public Search()
        {
            InitializeComponent();
        }

        public Search(PatientInfo patientInfo, List<string> painetList)
        {
            _patientInfo = patientInfo;
            _painetList = painetList;//从父级窗口带过来的已经选的症状
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(_painetList != null && _painetList.Any())
            {
                foreach(var item in _painetList)
                {
                    _symptomList.Add(item);
                }
            }



            symptomListTiemsControl.ItemsSource = _symptomList;
        }


        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

            Button btn = sender as Button;
            var str = btn.Tag.ToString();

            if (_symptomList.Contains(str))
            {
                _symptomList.Remove(str);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var diseaseName = diseaseNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(diseaseName)) 
            {
                MessageBox.Show("疾病名称不能为空!", "错误");
                return;
            }
            if(_symptomList==null || !_symptomList.Any())
            {
                MessageBox.Show("常见症状不能为空!", "错误");
                return;
            }
            var symptomString = "";
            foreach (var item in _symptomList)
            {
                symptomString += item;
            }

            var result = WebApiService.AddDisease(diseaseName, symptomString, _patientInfo.outpatientId);
            if (result)
            {
                MessageBox.Show("添加成功!", "成功");
                this.Close();
            }
            else
            {
                MessageBox.Show("添加失败!", "错误");
            }            
        }
        
        
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var s = sTextBox.Text.Trim();
            if (string.IsNullOrEmpty(s)) 
            {
                return;
            }

            if (_symptomList.Contains(s))
            {
                MessageBox.Show("列表中已经包含该症状!", "提示");
                return;
            }

            _symptomList.Add(s);

            sTextBox.Text = "";
            sTextBox.Focus();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
