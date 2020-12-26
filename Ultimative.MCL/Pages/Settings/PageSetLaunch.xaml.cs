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

namespace Ultimative.MCL.Pages
{
    /// <summary>
    /// PageSetLaunch.xaml 的交互逻辑
    /// </summary>
    public partial class PageSetLaunch : Page
    {
        public PageSetLaunch()
        {
            InitializeComponent();

            JavaPathGroup.SelectionChanged += delegate
            {
                var homeTag = (JavaHome)((RadioButton)JavaPathGroup.SelectedItem).Tag;

                Launcher.JavaPath = homeTag;
                JavaSelectionTextBlock.Text = homeTag.name;
            };

            foreach (JavaHome java in Launcher.JavaPaths)
            {
                var nameTextBlock = new TextBlock()
                {
                    Text = java.name,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                var pathTextBlock = new TextBlock()
                {
                    Text = java.path,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Foreground = new SolidColorBrush(Colors.Gray)
                };

                Grid.SetColumn(pathTextBlock, 1);

                JavaPathGroup.Items.Add(new RadioButton()
                {
                    Content = new Grid()
                    {
                        ColumnDefinitions =
                        {
                            new ColumnDefinition() { Width = new GridLength(80)},
                            new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star)}
                        },
                        Children =
                        {
                            nameTextBlock,
                            pathTextBlock
                        }
                    },
                    Tag = java
                });
            }
        }
    }
}
