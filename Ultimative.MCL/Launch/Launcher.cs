using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ultimative.MCL.Launch
{
    public partial class Launcher
    {
        private static int minMemory;
        private static int maxMemory;
        private static string javaPath;
        private static bool doNotCheckFiles;
        private static bool doNotCompleteFiles;
        private static bool showDiagnosticLog;
        private static int windowWidth;
        private static int windowHeight;
        private static bool windowFullscreen;

        public static int MinRam
        {
            get { return minMemory; }
            set
            {
                minMemory = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static int MaxRam
        {
            get { return maxMemory; }
            set
            {
                maxMemory = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static string JavaPath
        {
            get { return javaPath; }
            set
            {
                javaPath = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static bool DoNotCheckFiles
        {
            get { return doNotCheckFiles; }
            set
            {
                doNotCheckFiles = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static bool DoNotCompleteFiles
        {
            get { return doNotCompleteFiles; }
            set
            {
                doNotCompleteFiles = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static bool ShowDiagnosticLog
        {
            get { return showDiagnosticLog; }
            set
            {
                showDiagnosticLog = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static int GameWindowWidth
        {
            get { return windowWidth; }
            set
            {
                windowWidth = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static int GameWindowHeight
        {
            get { return windowHeight; }
            set
            {
                windowHeight = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static bool WindowFullscreen
        {
            get { return WindowFullscreen; }
            set
            {
                windowFullscreen = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

        public Launcher()
        {

        }

        public void Launch()
        {

        }
    }
}
