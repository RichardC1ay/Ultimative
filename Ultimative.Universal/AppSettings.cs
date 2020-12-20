using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ultimative.Universal
{
    public static class AppSettings
    {
        public static bool AlwaysOnTop { get; set; }
        public static bool WindowMaximized { get; set; }
        public static bool AllowShadow { get; set; }
        public static Grid RootPanel { get; set; }
        public static int ShadowRadius
        {
            get { return _shadowRadius; }
            set
            {
                _shadowRadius = value;
                if (StaticPropertyChanged != null)
                    StaticPropertyChanged(null, new PropertyChangedEventArgs("ShadowRadius"));
            }
        }
        public static Color AccentColor
        {
            get { return _accentColor; }
            set
            {
                _accentColor = value;
                if (StaticPropertyChanged != null)
                    StaticPropertyChanged(null, new PropertyChangedEventArgs("AccentColor"));
            }
        }

        private static int _shadowRadius;
        private static Color _accentColor;

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

        static AppSettings()
        {
            AlwaysOnTop = false;
            WindowMaximized = false;
            ShadowRadius = 0;
        }
    }

    
}
