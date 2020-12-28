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
            var arguments = manifest.GetValue("arguments").ToObject<JObject>();

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

            JArray gameArgs = JArray.Parse(arguments.GetValue("game").ToString());

            Packages.Add(new Package(
                "Main",
                manifest.GetValue("mainClass").ToObject<string>(),
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

        public Package(string id, string mainClass, string[] arguments)
        {
            Id = id;
            MainClass = mainClass;
            Arguments = new List<string>(arguments);
            Libraries = new List<Library>();
            Time = DateTime.Now;
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
