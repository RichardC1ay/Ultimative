using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ultimative.MCL
{
    public static class IndependentOptions
    {
        private static bool autoUpdate;
        private static bool selectAccountBeforeLaunch;
        private static bool selectAccBeforeLaunchQs;
        private static bool customQuickStart;
        private static bool useQuickStart;

        public static bool AutoUpdate
        {
            get
            {
                return autoUpdate;
            }
            set
            {
                autoUpdate = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static bool SelectAccountBeforeLaunch
        {
            get
            {
                return selectAccountBeforeLaunch;
            }
            set
            {
                selectAccountBeforeLaunch = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static bool SelectAccBeforeLaunchQs
        {
            get
            {
                return selectAccBeforeLaunchQs;
            }
            set
            {
                selectAccBeforeLaunchQs = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static bool CustomQuickStart
        {
            get
            {
                return customQuickStart;
            }
            set
            {
                customQuickStart = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static bool UseQuickStart
        {
            get
            {
                return useQuickStart;
            }
            set
            {
                useQuickStart = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }
    }
}
