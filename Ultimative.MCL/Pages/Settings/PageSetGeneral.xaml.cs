﻿using System;
using System.Collections.Generic;
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

namespace Ultimative.MCL.Pages
{
    /// <summary>
    /// PageSetGeneral.xaml 的交互逻辑
    /// </summary>
    public partial class PageSetGeneral : Page
    {
        public PageSetGeneral()
        {
            InitializeComponent();
        }

        private void LangComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var _langBox = (ComboBox)sender;

            _langBox.Items.Add("English (English)");
            _langBox.Items.Add("简体中文 (Simplified Chinese)");
            _langBox.Items.Add("繁體中文 (Traditional Chinese)");
        }
    }
}
