using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimative.MCL.Launch
{
    public partial class Launcher
    {
        public static int MaxMemory { get; set; }
        public static int MinMemory { get; set; }
        public static bool FullScreen { get; set; }
        public static int WindowHeight { get; set; }
        public static int WindowWidth { get; set; }

        static Launcher()
        {
            MaxMemory = 4096;
            MinMemory = 1024;
        }
    }
}
