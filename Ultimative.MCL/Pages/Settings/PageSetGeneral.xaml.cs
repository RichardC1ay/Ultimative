using System;
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
using Ultimative.Universal;

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

            LangComboBox.SelectedItem = AppSettings.DisplayLanguage;
        }

        private void LangComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var _langBox = (ComboBox)sender;

            foreach (Language lang in AppSettings.LoadedLanguage)
            {
                _langBox.Items.Add(lang);
            }
        }

        private void LangComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AppSettings.SetDisplayLanguage(LangComboBox.SelectedItem as Language);
        }
    }
}
