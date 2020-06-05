using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System.Collections.Generic;
using System.IO;
using System;

namespace apple.Infrastructure
{
    public class MangerConfig
    {
        public static IConfigurationRoot Configuration { get; }

        static MangerConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

            Configuration = builder.Build();
            var one = Configuration.GetValue<List<ConfigQuartzCms>>("QuartzConfig:quartzs");
            Configuration.Bind("QuartzConfig:quartzs", quartzconfig);
        }

        #region 要绑定的参数



        public static string sqlserverquartz => Configuration.GetConnectionString("sqlserverquartz");

        public static List<ConfigQuartzCms> quartzconfig { get; } = new List<ConfigQuartzCms>();

        public static bool IsUseproxy => ConvertBool("QuartzConfig:quartzaddress:IsUseproxy", "false");

        public static string localIp => ConvertString("QuartzConfig:quartzaddress:localIp", "");

        public static string quartzlocalIp => Configuration["QuartzConfig:quartzaddress:localIp"];
        public static string quartzchannelType => Configuration["QuartzConfig:quartzaddress:channelType"];

        public static string quartzport => Configuration["QuartzConfig:quartzaddress:port"];

        public static string quazrbindName => Configuration["QuartzConfig:quartzaddress:bindName"];




        public static string ConvertString(string key, string defaultvalue = "")
        {
            var value = Configuration[key];
            if (value.Length == 0 && !string.IsNullOrEmpty(defaultvalue))
                return defaultvalue;
            return value.Trim();
        }

        public static int ConvertInt(string key, string defaultvalue = "")
        {
            return Convert.ToInt32(ConvertString(key, defaultvalue));
        }

        public static bool ConvertBool(string key, string defaultvalue = "")
        {
            return Convert.ToBoolean(ConvertString(key, defaultvalue));
        }
        #endregion
    }

    #region 配置实体
    public class ConfigQuartzCms
    {
        public string name { get; set; }

        public string value { get; set; }
    }
    #endregion
}
