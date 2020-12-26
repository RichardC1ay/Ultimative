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
    /// DialogCreateAccount.xaml 的交互逻辑
    /// </summary>
    public partial class DialogEditAccount : ContentDialog
    {
        public Account EditingAccount;

        public DialogEditAccount()
        {
            InitializeComponent();
        }

        public DialogEditAccount(Account currentEditing)
        {
            InitializeComponent();

            this.EditingAccount = currentEditing;

            NameBox.Text = currentEditing.NameOrEmail;
            PasswdBox.Text = currentEditing.Password;
            switch (currentEditing.AuthMode)
            {
                case AuthMode.Offline:
                    OfflineRadioButton.IsChecked = true;
                    break;
                case AuthMode.Online:
                    OnlineRadioButton.IsChecked = true;
                    break;
                case AuthMode.AuthIntellij:
                    OAuthRadioButton.IsChecked = true;
                    break;
            }
        }

        private void Dialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if(args.Result == ContentDialogResult.Primary)
            {

                var _nameOrEmail = NameBox.Text;
                var _password = PasswdBox.Text;
                var _authMode = AuthMode.Offline;

                if ((bool)OfflineRadioButton.IsChecked)
                {
                    _authMode = AuthMode.Offline;
                }
                else if ((bool)OnlineRadioButton.IsChecked)
                {
                    _authMode = AuthMode.Online;
                }
                else
                {
                    _authMode = AuthMode.AuthIntellij;
                }

                if (EditingAccount != null)
                {
                    if (EditingAccount.Name != _nameOrEmail && MclCore.IsNameExists(_nameOrEmail))
                        return;

                    EditingAccount.NameOrEmail = _nameOrEmail;
                    EditingAccount.Password = _password;
                    EditingAccount.AuthMode = _authMode;
                }
                else
                {
                    if(MclCore.IsNameExists(_nameOrEmail))
                    {
                        this.Closed += CreateFailed;
                        return;
                    }
                    EditingAccount = new Account(
                        _authMode,
                        _nameOrEmail,
                        _password
                    );

                }
            }
        }

        private void CreateFailed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            new ContentDialog()
            {
                Title = "Create failed",
                Content = "This name or email already exists!",
                CloseButtonText = "Done",
                DefaultButton = ContentDialogButton.Close
            }.ShowAsync();
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _newText = NameBox.Text;

            if (EditingAccount != null && EditingAccount.NameOrEmail.Equals(_newText))
                return;

            AlreadyExistsTextBlock.Visibility = 
                MclCore.IsNameExists(_newText)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void PasswdBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _newPassword = PasswdBox.Text;

            PasswordRequiredTextBlock.Visibility =
                _newPassword == "" && PasswordHeaderContent.IsEnabled
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}
