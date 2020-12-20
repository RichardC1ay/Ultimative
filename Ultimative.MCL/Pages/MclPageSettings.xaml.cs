using ModernWpf.Media.Animation;
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

namespace Ultimative.MCL.Pages
{
    /// <summary>
    /// MclPageSettings.xaml 的交互逻辑
    /// </summary>
    public partial class MclPageSettings : Page
    {
        public MclPageSettings()
        {
            InitializeComponent();

            AddMenuItem(typeof(PageSetGeneral), "General");
            AddMenuItem(typeof(PageSetAppearance), "Appearance");
            AddMenuItem(typeof(PageSetInternet), "Internet");
            AddMenuItem(typeof(PageSetLaunch), "Launch");
            AddMenuItem(typeof(PageSetAbout), "About");

            ((ModernWpf.Controls.NavigationViewItem)SettingNavigation.MenuItems[0]).IsSelected = true;

            InitGenralSettings();
            InitAppearanceSettings();
        }

        private void AddMenuItem(Type type, string name)
        {
            SettingNavigation.MenuItems.Add(new ModernWpf.Controls.NavigationViewItem()
            {
                Content = name,
                Tag = type
            });
        }

        private async void ResetAllSettingsButtonClicked(object sender, RoutedEventArgs e)
        {
            var result = await new ModernWpf.Controls.ContentDialog()
            {
                Title = "Reset everything?",
                Content = "This operation is not reversible!",
                PrimaryButtonText = "Confirm",
                CloseButtonText = "Cancel",
                DefaultButton = ModernWpf.Controls.ContentDialogButton.Primary
            }.ShowAsync();
        }

        private void NavigationView_SelectionChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            ContentFrame.Navigate(((ModernWpf.Controls.NavigationViewItem)args.SelectedItem).Tag as Type, null, new EntranceNavigationTransitionInfo());
        }
    }
}
