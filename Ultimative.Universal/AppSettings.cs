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
        public static Grid RootPanel { get; set; }
        public static bool AutoUpdate { get; set; }

#pragma warning disable CS0067 // 从不使用事件“AppSettings.StaticPropertyChanged”
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
#pragma warning restore CS0067 // 从不使用事件“AppSettings.StaticPropertyChanged”

        static AppSettings()
        {
            AlwaysOnTop = false;
            WindowMaximized = false;
            AutoUpdate = false;
        }
    }

    
}
