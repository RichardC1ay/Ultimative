using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ultimative.Universal;

namespace Ultimative.Universal
{
    public static class AppCenter
    {

        public static void SetWindowMaximized()
        {

        }

        public static void SwitchMaximizeAndNormal()
        {
            bool windowMaximized = (AppSettings.WindowMaximized = !AppSettings.WindowMaximized);
            Application.Current.MainWindow.WindowState = windowMaximized ? WindowState.Maximized : WindowState.Normal;
            AppSettings.RootPanel.Margin = new Thickness(windowMaximized ? 8 : 0);
        }
    }
}
