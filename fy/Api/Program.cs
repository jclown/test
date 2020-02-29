using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Modobay;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region 调试新加密算法
            //var key = "2b3748c81f044866b5de23301fa93e54";
            //var content = "admin";
            //string str1 = Lib.EncryptHelper.EncryptAES(content, key);
            //string str2 = Lib.EncryptHelper.DecryptAES(str1, key);
            #endregion

            RunArgs.Url = args.Length >= 1 ? args[0] : string.Empty;
            RunArgs.Environment = args.Length >= 2 ? args[1] : string.Empty;
            BuildWebHost(args).Run();
        }

        private static string configFile;

        public static string ConfigFile
        { 
            get
            {
                if (string.IsNullOrEmpty(configFile)) configFile = GetConfigFile();
                return configFile;
            }
        }

        private static string GetConfigFile()
        {
            if (!string.IsNullOrEmpty(RunArgs.Environment)) return RunArgs.Environment;

            const string Environment_File_Test = "appsettings.Test.json";
            const string Environment_File_Prod = "appsettings.Prod.json";
            const string Environment_File_Dev = "appsettings.Development.json";

            if (File.Exists(Environment_File_Prod)) return Environment_File_Prod;
            if (File.Exists(Environment_File_Test)) return Environment_File_Test;
            return Environment_File_Dev;
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var configPath = Path.Combine(Lib.IOHelper.AppPath ?? Directory.GetCurrentDirectory(), ConfigFile);
            var appConfigPath = Path.Combine(Lib.IOHelper.AppPath ?? Directory.GetCurrentDirectory(), "appsettings.json");            
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile(configPath, optional: true, reloadOnChange: true);
            configBuilder.AddJsonFile(appConfigPath, optional: true, reloadOnChange: true);
            var config = configBuilder.Build();
            
            AppManager.ServiceAddress = AppManager.GetServiceAddress(RunArgs.Url);
            var isIDE = (RunArgs.Url == "%LAUNCHER_ARGS%");
            var builder = WebHost.CreateDefaultBuilder(args)
                    .UseJexusIntegration()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseConfiguration(config);                    

            if ((config["UseKestrel"] == "1" || RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) && !isIDE)
             {
                Lib.Log.WriteOperationLog($"Api-{AppManager.ServiceAddress}  {ConfigFile} Loading....UseKestrel");
                //var maxConcurrentConnections = 10000;
                //if (!string.IsNullOrEmpty(config["Gateway:MaxConcurrentConnections"]))
                //{
                //    int.TryParse(config["Gateway:MaxConcurrentConnections"], out maxConcurrentConnections);
                //}
                //builder.UseKestrel(option => option.Limits.MaxConcurrentConnections = maxConcurrentConnections);                
                builder.UseKestrel();
            }
            else
            {
                Lib.Log.WriteOperationLog($"Api-{AppManager.ServiceAddress}  {ConfigFile} Loading....UseIIS");
                builder.UseIIS(); 
            }

            if (!string.IsNullOrEmpty(RunArgs.Url) && !isIDE && !RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Lib.Log.WriteOperationLog($"Api-{AppManager.ServiceAddress}  {ConfigFile} UseUrl");
                builder.UseUrls(AppManager.ServiceAddress);
            }

            return builder.UseStartup<Startup>().Build();
        }        
    }
}
