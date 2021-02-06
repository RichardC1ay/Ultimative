using Microsoft.Win32;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Ultimative.MCL.Launch;
using Ultimative.Universal;
using Ultimative.Universal.Utilities;

namespace Ultimative.MCL
{
    public static class MclCore
    {
        private static ObservableCollection<Account> accounts;
        private static ObservableCollection<Launch.Version> versions;
        private static Account _usingAccount;
        private static SimpleStackPanel accountPanel;

        public static SimpleStackPanel AccountPanel
        {
            get { return accountPanel; }
            set
            {
                if (accountPanel == value)
                    return;

                if (accountPanel != null)
                    accountPanel.Children.Clear();

                accountPanel = value;
                foreach (Account account in accounts)
                {
                    accountPanel.Children.Add(account);
                }
            }
        }

        public static ObservableCollection<Account> Accounts
        {
            get { return accounts; }
        }

        public static ObservableCollection<Launch.Version> Versions
        {
            get { return versions; }
        }

        public static Account UsingAccount
        {
            get { return _usingAccount; }
            set
            {
                if(_usingAccount != null)
                {
                    if (_usingAccount == value)
                        return;

                    _usingAccount.IsUsing = false;
                }
                _usingAccount = value;

                if(value != null)
                    _usingAccount.IsUsing = true;
            }
        }

        public static Launcher GameLauncher { get; private set; }

        static MclCore()
        {
            //初始化变量
            accounts = new ObservableCollection<Account>();
            
            GameLauncher = new Launcher();

            IndependentOptions.SelectAccountBeforeLaunch = false;
        }

        public static bool IsNameExists(string nameOrEmail)
        {
            if (nameOrEmail == null || nameOrEmail.Equals(""))
                return false;

            foreach (Account account in accounts)
            {
                if (nameOrEmail.Equals(account.NameOrEmail))
                    return true;
            }
            return false;
        }

        public static void AddVersion(Launch.Version version)
        {
            Versions.Add(version);
        }

        public static void AddAccount(Account account)
        {
            Accounts.Add(account);

            AccountPanel.Children.Add(account);
        }

        public static void RemoveAllAccounts()
        {
            MclCore.UsingAccount = null;
            MclCore.Accounts.Clear();
            NotifyStaticPropertyChanged();

            AccountPanel.Children.Clear();
        }

        public static void RemoveAccount(Account account)
        {
            Accounts.Remove(account);

            if (UsingAccount == account)
            {
                System.Diagnostics.Debug.WriteLine(Accounts.First().NameOrEmail);
                UsingAccount = Accounts.First();
            }

            AccountPanel.Children.Remove(account);
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }
    }
}
