using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Ultimative.MCL.Pages.Dialogs;

namespace Ultimative.MCL.Launch
{
    /// <summary>
    /// AccountDisplayPanel.xaml 的交互逻辑
    /// </summary>
    public partial class Account : UserControl, INotifyPropertyChanged
    {
        private AuthMode authMode = AuthMode.Offline;
        private string nameOrEmail = "NoName";
        private string password;
        private string token;
        private bool isUsing = false;

        public Account()
        {
            InitializeComponent();
        }

        public Account(AuthMode paramAuthMode, string paramNameOrEmail, string paramPassword)
        {
            InitializeComponent();

            AuthMode = paramAuthMode;
            NameOrEmail = paramNameOrEmail;
            Password = paramPassword;
        }

        public AuthMode AuthMode
        {
            get { return authMode; }
            set
            {
                authMode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AuthMode)));
            }
        }

        public string NameOrEmail
        {
            get { return nameOrEmail; }
            set
            {
                nameOrEmail = value;
                NameOrEmailTextBlock.Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NameOrEmail)));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }

        public string Token
        {
            get { return token; }
            set
            {
                token = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Token)));
            }
        }

        public bool IsUsing
        {
            get { return isUsing; }
            set
            {
                isUsing = value;
                ItemIsSelected.IsChecked = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUsing)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ItemSelectToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            MclCore.UsingAccount = this;
        }

        private void ItemSelectToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            MclCore.UsingAccount = null;
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            await new DialogEditAccount(this).ShowAsync();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await new ContentDialog()
            {
                Title = "This operation is not reversible!",
                Content = NameOrEmail + " will be deleted permanently.",
                PrimaryButtonText = "Confirm",
                CloseButtonText = "Close",
                DefaultButton = ContentDialogButton.Primary
            }.ShowAsync();

            if(result == ContentDialogResult.Primary)
            {
                MclCore.RemoveAccount(this);
            }
        }
    }

    public enum AuthMode
    {
        Offline,
        Online,
        AuthIntellij
    }
}
