using System;
using System.Collections.Generic;
using System.Text;
using apple.Infrastructure;
using apple.data.Quartz;
using System.Runtime.Loader;
using System.Reflection;
using System.IO;
using System.Threading;
using Quartz;
using System.Threading.Tasks;
using apple.model.quarzt;
using apple.core;
namespace apptesttwo
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("2");
            //var jianting = new ManagerScheduler().Scheduler;

            //jianting.Start().Wait();




            MangerLog log = new MangerLog();
            log.Error("log" + DateTime.Now.ToLongDateString());
            MangerQuartznet manger = new MangerQuartznet();
            QuatzjobRepostory _quartzrepository = new QuatzjobRepostory();
            var job = _quartzrepository.FindById(4);
            manger.RunJob(job);

            Console.ReadKey();
        }
    }
}
