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
        public IConfigurationRoot Configuration { get; }
        bool _isweb = true;
        public MangerConfig(bool isweb=true)
        {
            var builder = new ConfigurationBuilder();
            if (isweb)
            {
                builder.SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            }
            else
            {
                builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables();
            }
            Configuration = builder.Build();
            var one = Configuration.GetValue<List<ConfigQuartzCms>>("QuartzConfig:quartzs");
            Configuration.Bind("QuartzConfig:quartzs", quartzlist);
        }
        public MangerConfig():this(false)
        {
           
        }
        #region 要绑定的参数



        public string sqlserverquartz => Configuration.GetConnectionString("sqlserverquartz");

        public List<ConfigQuartzCms> quartzlist { get; } = new List<ConfigQuartzCms>();

        public bool IsUseproxy => ConvertBool("QuartzConfig:quartzaddress:IsUseproxy", "false");

        public string localIp => ConvertString("QuartzConfig:quartzaddress:localIp", "");

        public string quartzlocalIp => Configuration["QuartzConfig:quartzaddress:localIp"];
        public string quartzchannelType => Configuration["QuartzConfig:quartzaddress:channelType"];

        public string quartzport => "520";// Configuration["QuartzConfig:quartzaddress:port"];

        public string quartzbindName => Configuration["QuartzConfig:quartzaddress:bindName"];




        public string ConvertString(string key, string defaultvalue = "")
        {
            var value = Configuration[key];
            if (value.Length == 0 && !string.IsNullOrEmpty(defaultvalue))
                return defaultvalue;
            return value.Trim();
        }

        public int ConvertInt(string key, string defaultvalue = "")
        {
            return Convert.ToInt32(ConvertString(key, defaultvalue));
        }

        public bool ConvertBool(string key, string defaultvalue = "")
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
