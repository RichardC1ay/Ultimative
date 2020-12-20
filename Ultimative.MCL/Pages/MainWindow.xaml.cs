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
using Ultimative.Universal;
using Ultimative.Utilities;

namespace Ultimative.MCL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RadioButton sideOptionLaunchPageBtn;
        RadioButton sideOptionAccountSwitchBtn;
        RadioButton sideOptionVersionControlBtn;
        RadioButton sideOptionSettingsBtn;
        ModernWpf.Controls.Frame frameContentNavigator;

        public MainWindow()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.Fant);
        }

        public override void OnApplyTemplate()
        {
            UpdateLayout();

            AppSettings.RootPanel = GetTemplateChild("RootPanel") as Grid;
            ContentPresenter sideOptionsPresenter = GetTemplateChild("SideOptionsPresenter") as ContentPresenter;
            Grid sideOptionsPanel = GetTemplateChild("SideOptionsPanel") as Grid;

            sideOptionLaunchPageBtn = TemplateHelper.GetVisualChild<RadioButton>((DependencyObject)sideOptionsPresenter.Content, v => v.Name == "SideOptionLaunchPageBtn");
            sideOptionAccountSwitchBtn = TemplateHelper.GetVisualChild<RadioButton>((DependencyObject)sideOptionsPresenter.Content, v => v.Name == "SideOptionAccountSwitchBtn");
            sideOptionVersionControlBtn = TemplateHelper.GetVisualChild<RadioButton>((DependencyObject)sideOptionsPresenter.Content, v => v.Name == "SideOptionVersionControlBtn");
            sideOptionSettingsBtn = TemplateHelper.GetVisualChild<RadioButton>((DependencyObject)sideOptionsPresenter.Content, v => v.Name == "SideOptionSettingsBtn");
            frameContentNavigator = GetTemplateChild("contentFrame") as ModernWpf.Controls.Frame;

            sideOptionLaunchPageBtn.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(OnSideOptionChanged);
            sideOptionAccountSwitchBtn.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(OnSideOptionChanged);
            sideOptionVersionControlBtn.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(OnSideOptionChanged);
            sideOptionSettingsBtn.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(OnSideOptionChanged);

            OnSideOptionChanged(sideOptionLaunchPageBtn, null);
        }

        
    }
}
