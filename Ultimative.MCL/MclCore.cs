using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ultimative.MCL.Launch;

namespace Ultimative.MCL
{
    public static class MclCore
    {
        private static List<Account> _accounts;

        public static List<Account> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                NotifyStaticPropertyChanged();
            }
        }

        static MclCore()
        {
            _accounts = new List<Account>();

            Accounts.Add(new Account(AuthMode.Offline, "Test", ""));
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }
    }
}
