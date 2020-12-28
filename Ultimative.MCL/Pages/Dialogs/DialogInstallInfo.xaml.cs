using ModernWpf.Controls;
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
using Ultimative.MCL.Launch;

namespace Ultimative.MCL.Pages.Dialogs
{
    /// <summary>
    /// DialogInstallInfo.xaml 的交互逻辑
    /// </summary>
    public partial class DialogInstallInfo : ContentDialog
    {
        public DialogInstallInfo(MinecraftVersion version)
        {
            InitializeComponent();

            VerNamePresenter.Text = "Current version: " + version.Id;

            verNameBox.Text = version.Id;
        }

        private void verNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsPrimaryButtonEnabled = verNameBox.Text != "";
        }
    }
}
