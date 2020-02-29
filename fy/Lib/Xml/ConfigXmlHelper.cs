using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Lib.Xml
{
    public class ConfigXmlHelper
    {

        /// <summary>  
        ///     更新配置信息，将配置信息对象序列化至相应的配置文件中
        /// </summary>  
        /// <typeparam name="T">配置信息类</typeparam>  
        /// <param name="config">配置信息</param>  
        public static void UpdateConfig<T>( T config,string path="")
        {
            Type configClassType = typeof(T);
            string configFilePath = GetConfigPath<T>( path).EnsureDirectory(); //根据配置文件名读取配置文件    
            try
            {
               
                using (var xmlTextWriter = new XmlTextWriter(configFilePath, Encoding.UTF8))
                {
                    var xmlSerializer = new XmlSerializer(configClassType);
                    xmlTextWriter.Formatting = Formatting.Indented;
                    var xmlNamespace = new XmlSerializerNamespaces();
                    xmlNamespace.Add(string.Empty, string.Empty);
                    xmlSerializer.Serialize(xmlTextWriter, config, xmlNamespace);
                } 
            }
            catch (SecurityException ex)
            {
                throw new GHException(ex.Message);
            }
        }

        /// <summary>  
        ///     获取配置信息  
        /// </summary>  
        /// <typeparam name="T">配置信息类</typeparam>  
        /// <returns>配置信息</returns>  
        public static T GetConfig<T>(string path="") where T : class, new()
        {
            var configObject = new object();
            Type configClassType = typeof(T);
            string configFilePath = GetConfigPath<T>(path); //根据配置文件名读取配置文件    
            if (File.Exists(configFilePath))
            {
                using (var xmlTextReader = new XmlTextReader(configFilePath))
                {
                    var xmlSerializer = new XmlSerializer(configClassType);
                    configObject = xmlSerializer.Deserialize(xmlTextReader);
                } 
            }
            var config = configObject as T;
            if (config == null)
            {
                return null;
            }
            return config;
        }



        /// <summary>  
        ///     获取配置文件的服务器物理文件路径  
        /// </summary>  
        /// <typeparam name="T">配置信息类</typeparam>  
        /// <returns>配置文件路径</returns>  
        public static string GetConfigPath<T>(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return IOHelper.CombinePath(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Config", $"{typeof(T).Name }.config");
            }
            else
            {
                return IOHelper.CombinePath(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Config", path, $"{typeof(T).Name }.config");
            }
           
        }


    }
}
