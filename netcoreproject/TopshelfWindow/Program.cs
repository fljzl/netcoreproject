using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using Topshelf;
using System.Linq;
using apple.Infrastructure;

namespace TopshelfWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<TraderService>();
                x.SetDescription("0desc");
                x.SetDisplayName("0quartznet");
                x.SetServiceName("0quartznetName");
                x.RunAsLocalService();
                x.OnException(ex =>
                {
                    Console.WriteLine(ex);
                    Singleton<MangerLog>.Instance.Infor("Program", ex);
                });
            });
            //Console.ReadKey();
        }
    }
}
