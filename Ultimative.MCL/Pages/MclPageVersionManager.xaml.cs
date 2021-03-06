﻿using ModernWpf.Media.Animation;
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
using Ultimative.Utilities;

namespace Ultimative.MCL.Pages
{
    /// <summary>
    /// MclPageVersionManager.xaml 的交互逻辑
    /// </summary>
    public partial class MclPageVersionManager : Page
    {
        public static ModernWpf.Controls.Frame StaticFrame { get; private set; }

        public MclPageVersionManager()
        {
            InitializeComponent();

            StaticFrame = ContentFrame;
            ContentFrame.Navigate(typeof(VersionList),
                null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
    }
}
