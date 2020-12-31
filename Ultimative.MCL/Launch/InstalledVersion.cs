using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimative.MCL.Launch
{
    public class InstalledVersion
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public AssetObject Core { get; set; }
        public AssetObject AssetIndex { get; set; }
        public string Assets { get; set; }
        public List<Package> Packages { get; }

        public InstalledVersion(JObject manifest)
        {
            Packages = new List<Package>();

            var assetIndexJson = (JObject)manifest.GetValue("assetIndex");
            var libraryJson = JArray.Parse(manifest.GetValue("libraries").ToString());
            var coreJson = ((JObject)manifest.GetValue("downloads")).GetValue("client").ToObject<JObject>();

            Id = manifest.GetValue("id").ToObject<string>();
            Type = manifest.GetValue("type").ToObject<string>();

            AssetIndex = new AssetObject()
            {
                FileSize = assetIndexJson.GetValue("size").ToObject<int>(),
                FileName = assetIndexJson.GetValue("id") + ".json",
                HttpUrl = assetIndexJson.GetValue("url").ToObject<string>(),
                SHA1Code = assetIndexJson.GetValue("sha1").ToObject<string>()
            };

            Core = new AssetObject()
            {
                FileSize = coreJson.GetValue("size").ToObject<int>(),
                HttpUrl = coreJson.GetValue("url").ToObject<string>(),
                SHA1Code = coreJson.GetValue("sha1").ToObject<string>()
            };

            Assets = manifest.GetValue("assets").ToObject<string>();

            string args;
            if (manifest.ContainsKey("game"))
            {
                StringBuilder sb = new StringBuilder();
                foreach (JToken itemToken in JArray.Parse(manifest.Value<JObject>("game").ToString()))
                    if (itemToken.Type == JTokenType.String)
                        sb.Append(itemToken.ToObject<string>()); 
            }
            else
                args = manifest.Value<string>("minecraftArguments");

            Packages.Add(new Package(
                "Main",
                manifest.GetValue("mainClass").ToObject<string>(),
                Environment.CurrentDirectory + "\\.minecraft\\versions\\" + Id,
                null 
            ));
        }
    }

    public struct Package
    {
        public string Id { get; set; }
        public string MainClass { get; set; }
        public List<string> Arguments { get; }
        public List<Library> Libraries { get; set; }
        public DateTime Time { get; }
        public string GamePath { get; set; }

        public Package(string id, string mainClass, string path, string[] arguments)
        {
            Id = id;
            MainClass = mainClass;
            Arguments = arguments == null ? null : new List<string>(arguments);
            Libraries = new List<Library>();
            Time = DateTime.Now;
            GamePath = path;
        }
    }

    public struct MinecraftVersion
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public DateTime Time { get; set; }
    }
}
