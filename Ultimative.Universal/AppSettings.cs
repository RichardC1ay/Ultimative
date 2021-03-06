﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ultimative.Universal
{
    public static class AppSettings
    {
        private static bool _alwaysOnTop = false;

        
        public static bool WindowMaximized { get; set; }
        public static Grid RootPanel { get; set; }
        public static bool AutoUpdate { get; set; }
        public static List<Language> LoadedLanguage { get; private set; }
        public static Language DisplayLanguage { get; private set; }

        public static bool AlwaysOnTop
        {
            get
            {
                return _alwaysOnTop;
            }
            set
            {
                _alwaysOnTop = value;
                NotifyStaticPropertyChanged();
            }
        }

        static AppSettings()
        {
            WindowMaximized = false;
            AutoUpdate = false;
            LoadedLanguage = new List<Language>();
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

        public static Language GetDisplayLanguage()
        {
            return DisplayLanguage;
        }

        public static bool SetDisplayLanguage(Language lang)
        {
            if(DisplayLanguage == lang)
            {
                return true;
            }

            ResourceDictionary _langDictionary = null;

            if(!LoadedLanguage.Contains(lang))
            {
                System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();

                string filepath = "/Assets/Languages/" + lang.FileName + ".xaml";
                if (File.Exists(filepath))
                {
                    try
                    {
                        _langDictionary = Application.LoadComponent(new Uri(filepath, UriKind.RelativeOrAbsolute)) as ResourceDictionary;
                        LoadedLanguage.Add(lang);
                    } 
#pragma warning disable CS0168 // 声明了变量“ex”，但从未使用过
                    catch (Exception ex)
#pragma warning restore CS0168 // 声明了变量“ex”，但从未使用过
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            _langDictionary = Application.LoadComponent(new Uri("/Assets/Languages/" + lang.FileName + ".xaml",
                UriKind.RelativeOrAbsolute)) as ResourceDictionary;
            ResourceDictionary _currentMergedDictionary = Application.Current.Resources;

            _currentMergedDictionary.MergedDictionaries.Add(_langDictionary);
            DisplayLanguage = lang;

            return true;
        }
    }

    public class Language
    {
        public string Name { get; private set; }
        public string FileName { get; private set; }
        public string Area { get; private set; }

        public Language(string name, string filename, string area)
        {
            this.Name = name;
            this.FileName = filename;
            this.Area = area;
        }

        public override string ToString()
        {
            return Name + " (" + Area + ')';
        }
    }
    
}
