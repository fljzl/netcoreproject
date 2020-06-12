using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using webwindows.bacservice;

namespace webwindows
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //Microsoft.Extensions.Hosting.WindowsServices.dll
        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).
             UseWindowsService()
             .ConfigureServices((hostContext, services) =>
             {
                 services.AddHostedService<ServiceA>();
                 services.AddHostedService<ServiceB>();
             })
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder
                   .UseUrls("http://*:5002", "http://www.sqlme.com:3200")//配置监听端口
                   .UseStartup<Startup>()
                   .UseKestrel();//指定托管服务器，Kestrel 或者iis服务器都可以，推荐使用Kestrel，可以在无iis的环境使用。
                 
             });
    }
}
