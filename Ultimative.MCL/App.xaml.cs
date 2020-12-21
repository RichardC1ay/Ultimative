using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ultimative.Universal;

namespace Ultimative.MCL
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            System.Reflection.Assembly appAssembly = System.Reflection.Assembly.GetExecutingAssembly();

            JsonTextReader reader = new JsonTextReader(new StreamReader(appAssembly.GetManifestResourceStream("Ultimative.MCL.Properties.lang.json")));
            JObject jsonObject = (JObject)JToken.ReadFrom(reader);

            JArray langArray = JArray.Parse(jsonObject.GetValue("language").ToString());
            foreach (JObject langJsonObject in langArray)
            {
                System.Diagnostics.Debug.WriteLine(langJsonObject.ToString());
                AppSettings.LoadedLanguage.Add(new Language(
                    langJsonObject.GetValue("name").ToObject(typeof(string)) as string,
                    langJsonObject.GetValue("fileName").ToObject(typeof(string)) as string,
                    langJsonObject.GetValue("area").ToObject(typeof(string)) as string
                ));
            }
        }
        
    }


}
