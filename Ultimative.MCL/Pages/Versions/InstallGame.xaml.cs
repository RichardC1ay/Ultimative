using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ultimative.MCL.Launch;
using Ultimative.Universal.Controls;

namespace Ultimative.MCL.Pages
{
    /// <summary>
    /// InstallGame.xaml 的交互逻辑
    /// </summary>
    public partial class InstallGame : Page
    {
        private string filterType;
        private string filterKeyword;
        public MinecraftVersion InstallVersion { get; private set; }

        public InstallGame()
        {
            InitializeComponent();
        }

        public void ShowItems()
        {
            VerItemList.Children.Clear();

            foreach (MinecraftVersion ver in Launcher.MinecraftVersions)
            {
                if (filterType != null && !ver.Type.Contains(filterType.ToLower()))
                    continue;
                if (filterKeyword != null && !ver.Id.Contains(filterKeyword))
                    continue;

                var IdText = new TextBlock()
                {
                    Text = ver.Id,
                    FontSize = 14,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(20, 0, 0, 0)
                };
                var TypeText = new TextBlock()
                {
                    Text = Launcher.GetVersionType(ver.Type),
                    FontSize = 14,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(20, 0, 0, 0)
                };
                var TimeText = new TextBlock()
                {
                    Text = ver.Time.ToLongDateString(),
                    FontSize = 14,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(20, 0, 0, 0)
                };

                var sep0 = ControlSet.GetSeparator(Orientation.Vertical);
                var sep1 = ControlSet.GetSeparator(Orientation.Vertical);

                sep0.HorizontalAlignment = HorizontalAlignment.Right;
                sep1.HorizontalAlignment = HorizontalAlignment.Right;

                Grid.SetColumn(TypeText, 1);
                Grid.SetColumn(TimeText, 2);
                Grid.SetColumn(sep1, 1);

                var btn = new Button()
                {
                    Style = (Style)FindResource("UFlatButton"),
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    Height = 32,
                    Padding = new Thickness(0),
                    Content = new Grid()
                    {
                        ColumnDefinitions =
                        {
                            new ColumnDefinition() { Width = new GridLength(150) },
                            new ColumnDefinition() { Width = new GridLength(150) },
                            new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }
                        },
                        Children =
                        {
                            IdText,
                            TypeText,
                            TimeText,
                            sep0,
                            sep1
                        },
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch
                    },
                    Tag = ver
                };
                btn.Click += new RoutedEventHandler(VersionButton_Click);
                VerItemList.Children.Add(btn);
            }
        }

        private void ResetFilter()
        {
            filterKeyword = null;
            filterType = null;

            FilterKeywordTextBlock.Text = "";
            foreach (RadioButton _radioButton in TypeGroup.Items)
                _radioButton.IsChecked = false;
        }

        private void VersionButton_Click(object sender, RoutedEventArgs e)
        {
            InstallVersion = (MinecraftVersion)((Button)sender).Tag;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ShowItems();

            ListRootGrid.Visibility = Visibility.Visible;
        }

        private void RefreshItems_Click(object sender, RoutedEventArgs e)
        {
            ListRootGrid.IsEnabled = false;
            ResetFilter();
            
            DoubleAnimation _animation = new DoubleAnimation(
                1.0d,
                0.0d,
                new Duration(TimeSpan.FromMilliseconds(480.0d))
            )
            {
                EasingFunction = new CubicEase(),
                BeginTime = TimeSpan.FromMilliseconds(200.0d)
            };
            _animation.Completed += delegate
            {
                ListRootGrid.Visibility = Visibility.Collapsed;

                new Thread(o =>
                {
                    Launcher.CheckVersionManifest();
                    Dispatcher.Invoke(new Action(delegate
                    {
                        filterKeyword = null;
                        filterType = null;
                        ShowItems();

                        ListRootGrid.Visibility = Visibility.Visible;
                        ListRootGrid.IsEnabled = true;
                        DoubleAnimation _reverseAnimation = new DoubleAnimation(
                            0.0d,
                            1.0d,
                            new Duration(TimeSpan.FromMilliseconds(480.0d))
                        )
                        {
                            EasingFunction = new CubicEase(),
                        };
                        ListRootGrid.BeginAnimation(StackPanel.OpacityProperty, _reverseAnimation);
                    }));
                })
                { IsBackground = true }.Start();
            };
            ListRootGrid.BeginAnimation(StackPanel.OpacityProperty, _animation);
        }

        private void Keyword_Apply(object sender, RoutedEventArgs e)
        {
            filterKeyword = FilterKeywordTextBlock.Text == ""
                ? null
                : FilterKeywordTextBlock.Text;

            ShowItems();
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var _radioButton = (RadioButton)TypeGroup.SelectedItem;
            if(_radioButton != null)
                filterType = _radioButton.Content as string;

            ShowItems();
        }
    }
}
