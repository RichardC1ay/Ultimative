using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimative.Universal.Utilities
{
    public class RegistryHelper
    {
        public static RegistryKey GetRegistryKey(RegistryHive hive)
        {
            return RegistryKey.OpenBaseKey(hive,
                Environment.Is64BitOperatingSystem
                ? RegistryView.Registry64
                : RegistryView.Registry32);
        }
    }
}
