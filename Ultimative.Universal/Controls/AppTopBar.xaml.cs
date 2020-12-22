using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ultimative.Universal.Controls
{
    /// <summary>
    /// AppTopBar.xaml 的交互逻辑
    /// </summary>
    public partial class AppTopBar : UserControl
    {
        public AppTopBar()
        {
            InitializeComponent();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                {
                    AppCenter.SwitchMaximizeAndNormal();
                }
                Application.Current.MainWindow.DragMove();
            }
        }

        private void OnAlwaysOnTopButtonClick(object sender, RoutedEventArgs e)
        {
            bool alwaysOnTopState = (bool)((ToggleButton)sender).IsChecked;
            Application.Current.MainWindow.Topmost = alwaysOnTopState;
            ButtonSvgAlwaysOnTop.Fill = FindResource(alwaysOnTopState
                ? "BackgroundTier2Brush"
                : "ForegroundBrush") as SolidColorBrush;
        }

        private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void OnMaximizeButtonClick(object sender, RoutedEventArgs e)
        {
            AppCenter.SwitchMaximizeAndNormal();
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
