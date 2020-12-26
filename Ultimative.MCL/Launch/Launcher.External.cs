using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ultimative.Universal.Utilities;

namespace Ultimative.MCL.Launch
{
    public partial class Launcher
    {
        private static ObservableCollection<JavaHome> javaPaths;
        

        private const string versionManifestUrl = "https://launchermeta.mojang.com/mc/game/version_manifest.json";

        public static JavaHome JavaPath
        {
            get { return javaPath; }
            set
            {
                javaPath = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static ObservableCollection<MinecraftVersion> MinecraftVersions { get; private set; }

        public static void CheckVersionManifest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(versionManifestUrl);
            {
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/json";
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            var jsonReader = new JsonTextReader(new StreamReader(response.GetResponseStream(), Encoding.UTF8));
            JObject jObject = (JObject)JToken.ReadFrom(jsonReader);

            MinecraftVersions.Clear();
            JArray verArray = JArray.Parse(jObject.GetValue("versions").ToString());
            foreach (JObject verJsonObject in verArray)
                MinecraftVersions.Add(new MinecraftVersion()
                {
                    Id = verJsonObject.GetValue("id").ToObject<string>(),
                    Type = verJsonObject.GetValue("type").ToObject<string>(),
                    Url = verJsonObject.GetValue("url").ToObject<string>(),
                    Time = DateTime.Parse(verJsonObject.GetValue("releaseTime").ToObject<string>())
                });
        }

        public static bool CheckJavaRuntime()
        {
            //
            javaPaths.Clear();

            /**
             * 按操作系统类型获取注册表键
             * 获取到的值是空值的解决办法
             * https://www.cnblogs.com/xinweichen/p/5276934.html
             */
            RegistryKey softwareRegistry = RegistryHelper.GetRegistryKey(RegistryHive.LocalMachine).
                OpenSubKey("SOFTWARE", RegistryKeyPermissionCheck.ReadSubTree);

            //检查是否安装了任意的Java
            var subKeys = softwareRegistry.GetSubKeyNames();
            if (!subKeys.Contains("JavaSoft"))
                return false;

            //分别获取jdk和jre的注册表路径
            var javaKey = softwareRegistry.OpenSubKey("JavaSoft", RegistryKeyPermissionCheck.ReadSubTree);
            var jre = javaKey.OpenSubKey("Java Runtime Environment", RegistryKeyPermissionCheck.ReadSubTree);
            var jdk = javaKey.OpenSubKey("JDK", RegistryKeyPermissionCheck.ReadSubTree);

            ReadJavaRegistry(jre); //判断是否有Java运行时
            ReadJavaRegistry(jdk); //判断是否有Java开发组件

            return javaPaths.Count != 0;
        }

        private static void ReadJavaRegistry(RegistryKey registry)
        {
            if (registry != null)
            {
                var vers = registry.GetSubKeyNames();
                foreach (string verName in vers)
                {
                    var homePath = registry.OpenSubKey(verName, false).GetValue("JavaHome");

                    JavaHome home = new JavaHome
                    {
                        name = verName,
                        path = (string)homePath + "\\bin\\javaw.exe"
                    };

                    javaPaths.Add(home);
                }
            }
        }
    }
}
