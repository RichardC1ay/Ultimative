using Microsoft.Win32;
using ModernWpf.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ultimative.Universal.Net;
using Ultimative.Universal.Utilities;

namespace Ultimative.MCL.Launch
{
    public partial class Launcher
    {
        private static ObservableCollection<JavaHome> javaPaths;
        private static ObservableCollection<Version> installedVers;

        private const string versionManifestUrl = "https://launchermeta.mojang.com/mc/game/version_manifest.json";
        private const string assetObjectUrl = "http://resources.download.minecraft.net/";
        public static readonly string MinecraftDir = Environment.CurrentDirectory + "\\.minecraft";
        public static readonly string AssetsDir = Environment.CurrentDirectory + "\\.minecraft\\assets";
        public static readonly string LibrariesDir = Environment.CurrentDirectory + "\\.minecraft\\libraries";
        public static readonly string VersionsDir = Environment.CurrentDirectory + "\\.minecraft\\versions";

        public static List<MultiFileTask> Tasks;

        public static JavaHome JavaPath
        {
            get { return javaPath; }
            set
            {
                javaPath = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static ObservableCollection<MinecraftVersion> MinecraftVersions 
        {
            get; 
            private set; 
        }

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

        public static void GetManifest(string httpUrl, string version)
        {
            var indexFolder = Environment.CurrentDirectory + "\\.minecraft\\versions\\" + version;
            var indexFile = indexFolder + "\\" + version + ".json";

            Directory.CreateDirectory(indexFolder);

            HttpFile.Download(httpUrl, indexFile, delegate {
                JsonTextReader textReader;
                try
                {
                    textReader = new JsonTextReader(new StreamReader(new FileStream(indexFile, FileMode.Open)));
                }
                catch (IOException ex)
                {
                    new ContentDialog()
                    {
                        Title = "Exception",
                        Content = ex.Message,
                        CloseButtonText = "Confirm"
                    }.ShowAsync();
                    return;
                }

                Install((JObject)JToken.ReadFrom(textReader));
            });
        }

        public static void Install(JObject manifest)
        {
            var version = new Version(manifest);

            installedVers.Add(version);
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                GetVersionCore(version);
                GetAssembly(version);
                GetLibraries(version, version.Packages.Find(o => o.Id == "game"));
            });
        }

        public static void GetAssembly(Version version)
        {
            AssetObject assetObject = version.AssetIndex;
            string indexDir = AssetsDir + "\\indexes";
            string index = indexDir + "\\" + assetObject.FileName;

            Directory.CreateDirectory(indexDir);
            HttpFile.Download(assetObject.HttpUrl, index, delegate
            {
                GetAssetResources((JObject)JToken.ReadFrom(
                    new JsonTextReader(
                        new StreamReader(
                            new FileStream(index, FileMode.Open)
                        )
                    )));
            });
        }

        public static void GetAssetResources(JObject objects)
        {
            var assetObjectsDir = AssetsDir + "\\objects";
            Directory.CreateDirectory(assetObjectsDir);

            var taskGuid = Guid.NewGuid().ToString();
            List<JObject> dlQueue = new List<JObject>();
            System.Diagnostics.Debug.WriteLine(objects.Value<JObject>("objects").Children().Count());
            foreach (JToken itemToken in objects.Value<JObject>("objects").Values())
            {
                var hash = itemToken.Value<string>("hash");
                System.Diagnostics.Debug.WriteLine(assetObjectsDir + "\\" + hash.Substring(0, 2) + "\\" + hash);
                if (!File.Exists(assetObjectsDir + "\\" + hash.Substring(0, 2) + "\\" + hash))
                    dlQueue.Add(itemToken.ToObject<JObject>());
            }

            MultiFileTask task = new MultiFileTask(taskGuid, dlQueue.Count);
            Tasks.Add(task);

            foreach (JObject item in dlQueue)
            {
                string hash = item.GetValue("hash").ToObject<string>();
                int size = item.GetValue("size").ToObject<int>();

                string hash2code = hash.Substring(0, 2);

                string parent = assetObjectsDir + "\\" + hash2code;
                string path = parent + "\\" + hash;

                Directory.CreateDirectory(parent);

                HttpFile.Download(assetObjectUrl + hash2code + '/' + hash, path, delegate { task.Done += 1; });
            }
        }

        public static void GetLibraries(Version version, string libName)
        {
            GetLibraries(version, version.Packages.Find(o => o.Id == "game"));
        }
        
        public static void GetLibraries(Version version, Package package)
        {

        }

        public static void GetVersionCore(Version version)
        {
            AssetObject coreAsset = version.Core;

            string directory = MinecraftDir + "\\versions\\" + version.Id + "\\";
            string corePath = directory + "\\" + version.Id + ".jar";

            if (File.Exists(corePath))
                if (coreAsset.SHA1Code == FileHelper.GetSHA1(corePath))
                    return;
                else
                    File.Delete(corePath);

            Directory.CreateDirectory(directory);
            HttpFile.Download(coreAsset.HttpUrl, corePath, null);
        }

    }
}
