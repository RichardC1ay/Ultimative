using ModernWpf.Media.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Ultimative.Universal;

namespace Ultimative.MCL
{
    partial class MainWindow
    {
        static RadioButton Selected { get; set; }

        public void OnSideOptionChanged(object sender, MouseButtonEventArgs e)
        {
            if (Selected != null)
            {
                if(Selected == sender)
                {
                    return;
                }
                Selected.Background = new SolidColorBrush(Color.FromArgb(0,0,0,0));
                StackPanel _selContent = Selected.Content as StackPanel;
                ((Path)((Viewbox)_selContent.Children[0]).Child).Fill = FindResource("UltimativeRadioButtonUnselectedBrush") as SolidColorBrush;
                ((TextBlock)_selContent.Children[1]).Foreground = FindResource("UltimativeRadioButtonUnselectedBrush") as SolidColorBrush; ;
            }
            var _selected = sender as RadioButton;
            Selected = _selected;
            _selected.Background = FindResource("BackgroundBrush") as SolidColorBrush;

            StackPanel content = _selected.Content as StackPanel;
            ((Path)((Viewbox)content.Children[0]).Child).Fill = FindResource("UltimativeRadioButtonSelectedBrush") as SolidColorBrush;
            ((TextBlock)content.Children[1]).Foreground = FindResource("UltimativeRadioButtonSelectedBrush") as SolidColorBrush;

            frameContentNavigator.Navigate(
                FrameAssist.GetFrameContent(_selected).GetType(),
                null,
                new DrillInNavigationTransitionInfo()
            //new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft }
            );
        }
    }
}
