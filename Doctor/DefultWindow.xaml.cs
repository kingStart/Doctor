﻿using Doctor.Model;
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
    public partial class DefultWindow : Window
    {
        private DoctorInfo _doctor;

        public DefultWindow(DoctorInfo doctor)
        {
            this._doctor = doctor;
            InitializeComponent();
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
        }
    }
}
