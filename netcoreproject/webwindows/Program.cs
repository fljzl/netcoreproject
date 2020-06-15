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
            .ConfigureLogging((buildercontext, loggingbuilder) =>
            {
                loggingbuilder.AddFilter("System", LogLevel.Warning); //���˵�ϵͳĬ�ϵ�һЩ��־
                loggingbuilder.AddFilter("Microsoft", LogLevel.Warning);//���˵�ϵͳĬ�ϵ�һЩ��־

                                                                        //var path = Directory.GetCurrentDirectory() + "\\log4net.config"; 
                                                                        //������������ʾlog4net.config�������ļ�����Ӧ�ó����Ŀ¼�£�Ҳ����ָ�������ļ���·��
                loggingbuilder.AddLog4Net();


            })
             .ConfigureServices((hostContext, services)
            =>
             {
                 services.AddHostedService<ServiceA>();
                 services.AddHostedService<ServiceB>();
             })
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder
                   .UseUrls("http://*:5002", "http://www.sqlme.com:3200")//���ü����˿�
                   .UseStartup<Startup>()
                   .UseKestrel();//ָ���йܷ�������Kestrel ����iis�����������ԣ��Ƽ�ʹ��Kestrel����������iis�Ļ���ʹ�á�

             });
    }
}
