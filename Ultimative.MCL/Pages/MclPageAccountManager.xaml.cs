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
using Ultimative.MCL.Pages.Dialogs;

namespace Ultimative.MCL.Pages
{
    /// <summary>
    /// MclPageAccountManager.xaml 的交互逻辑
    /// </summary>
    public partial class MclPageAccountManager : Page
    {
        public MclPageAccountManager()
        {
            InitializeComponent();

            MclCore.Accounts.CollectionChanged += delegate { ItemsControl.Items.Refresh(); };

        }

        private async void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var _dialog = new DialogEditAccount();

            await _dialog.ShowAsync();
        }

        private async void DeleteAllButton_Click(object sender, RoutedEventArgs e)
        {
            var _dialog = new ModernWpf.Controls.ContentDialog()
            {
                Title = "Delete all saved accounts?",
                Content = "This operation is not reversible!",
                PrimaryButtonText = "Confirm",
                CloseButtonText = "Cancel",
                DefaultButton = ModernWpf.Controls.ContentDialogButton.Primary
            };
            var result = await _dialog.ShowAsync();

            if(result == ModernWpf.Controls.ContentDialogResult.Primary)
            {
                MclCore.RemoveAllAccounts();
            }
        }
    }
}
