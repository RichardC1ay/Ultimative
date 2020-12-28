using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ultimative.Universal.Utilities;

namespace Ultimative.MCL.Launch
{
    public partial class Launcher
    {
        private static int minMemory;
        private static int maxMemory;
        private static JavaHome javaPath;
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

        public static ObservableCollection<JavaHome> JavaPaths { get { return javaPaths; } }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

        public static string GetVersionType(string str)
        {
            switch (str)
            {
                case "release":
                    return App.Current.FindResource("VersionType_Release") as string;
                case "snapshot":
                    return App.Current.FindResource("VersionType_Snapshot") as string;
                case "old_beta":
                    return App.Current.FindResource("VersionType_Old_Beta") as string;
                case "old_alpha":
                    return App.Current.FindResource("VersionType_Old_Alpha") as string;
                default:
                    return "unknown";
            }
        }

        static Launcher()
        {
            javaPaths = new ObservableCollection<JavaHome>();
            MinecraftVersions = new ObservableCollection<MinecraftVersion>();

            Directory.CreateDirectory(MinecraftDir);
            Directory.CreateDirectory(AssetsDir);
            Directory.CreateDirectory(LibrariesDir);
            Directory.CreateDirectory(VersionsDir);

            CheckJavaRuntime();
            new Thread(o => CheckVersionManifest()) { IsBackground = true }.Start();
        }

        public Launcher()
        {
            CheckJavaRuntime();
        }

        public void Launch()
        {

        }
    }

    public struct JavaHome
    {
        public string name;
        public string path;

        public override string ToString()
        {
            return name;
        }
    }
}
