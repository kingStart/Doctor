﻿using System;
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
   

    public partial class SearchText : UserControl
    {
        public SearchText()
        {
            InitializeComponent();
        }

        public event EventHandler<SearchEventArgs> OnSearch;
        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            ExeccuteSearch();
        }

        private void TbxInput_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TbxInput.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                if (!string.IsNullOrEmpty(TbxInput.Text))
                {
                    ExeccuteSearch();
                }
            }
        }

        private void ExeccuteSearch()
        {
            if (OnSearch != null)
            {
                var args = new SearchEventArgs();
                args.SearchText = TbxInput.Text;
                
                OnSearch(this, args);
                TbxInput.Text = "";
            }
        }

        public string GetText()
        {
            return TbxInput.Text;
        }
        public void ClearText()
        {
            TbxInput.Text = "";
        }
    }
    public class SearchEventArgs : EventArgs
    {
        public string SearchText { get; set; }
    }
}
