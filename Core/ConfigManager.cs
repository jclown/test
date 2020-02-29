using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Modobay
{
    public class ConfigManager
    {
        private static IConfiguration configuration = null;

        public static IConfiguration Configuration 
        {
            get { return configuration; }
            set 
            {
                configuration = value;
                ApiDocConfig = GetConfigStringItems("ApiDoc");
                AppConfig = GetConfigItems("AppConfig");
                AppApiDenyConfig = GetConfigStringItems("AppApiDeny");
            } 
        }
        public static Dictionary<string, dynamic> AppConfig { get; set; }
        public static Dictionary<string, List<string>> AppApiDenyConfig { get; set; }

        public static Dictionary<string, List<string>> ApiDocConfig { get; set; }

        public static dynamic GetConfigItem(string key)
        {
            return Lib.DynamicHelper.ToDynamic(Configuration[key]);
        }

        public static Dictionary<string, dynamic> GetConfigItems(string key)
        {
            var list = new Dictionary<string, dynamic>();

            try
            {
                var configSection = Configuration.GetSection(key);
                var children = configSection.GetChildren();

                foreach (var item in children)
                {
                    list.Add(item.Key, Lib.DynamicHelper.ToDynamic(item.Value));
                }
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"AppManager.AddServices:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }

            return list;
        }

       
        public static Dictionary<string, List<string>> GetConfigStringItems(string key)
        {
            var list = new Dictionary<string, List<string>>();

            try
            {
                var configSection = Configuration.GetSection(key);
                var children = configSection.GetChildren();

                foreach (var item in children)
                {
                    list.Add(item.Key, item.Value.ToSplitString());
                }
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"AppManager.AddServices:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }

            return list;
        }

        public static IConfigurationRoot GetConfig(string jsonFilePath)
        {
            var file = Lib.IOHelper.GetFileFullPath(jsonFilePath);

            var builder = new ConfigurationBuilder()
               .AddJsonFile(file, optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public static Dictionary<string, dynamic> GetConfig(Stream jsonFileContent)
        {
            var list = new Dictionary<string, dynamic>();

            try
            {
                var data = JsonConfigurationFileParser.Parse(jsonFileContent);
                foreach (var item in data)
                {
                    list.Add(item.Key, Lib.DynamicHelper.ToDynamic(item.Value));
                }
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"AppManager.AddServices:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }

            return list;
        }
    }
}
