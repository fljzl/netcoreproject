using apple.test.BaseTest;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace apple.test
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = TestFactory.GetMyTest(0);
            test.Log();


            #region 官网案列
            //LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            //RunProgramRunExample().GetAwaiter().GetResult();

            //Console.WriteLine("Press any key to close the application");
            #endregion


            Console.ReadKey();
        }

        private static async Task RunProgramRunExample()
        {
            try
            {
                var properties = new NameValueCollection();
                properties["quartz.scheduler.instanceName"] = "RemoteServer";
                properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
                properties["quartz.threadPool.threadCount"] = "500";
                properties["quartz.threadPool.threadPriority"] = "Normal";
                //properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
                properties["quartz.scheduler.exporter.port"] = "555";//端口号
                properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";//名称
                properties["quartz.scheduler.exporter.channelType"] = "tcp";//通道类型
                properties["quartz.scheduler.exporter.channelName"] = "httpQuartz";
                properties["quartz.scheduler.exporter.rejectRemoteRequests"] = "true";
                properties["quartz.jobStore.clustered"] = "true";//集群配置
                                                                 //指定quartz持久化数据库的配置
                properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";//存储类型
                properties["quartz.serializer.type"] = "json";
                properties["quartz.jobStore.tablePrefix"] = "Qrtz_";//表名前缀
                properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";//驱动类型
                properties["quartz.jobStore.dataSource"] = "myDS";//数据源名称
                properties["quartz.dataSource.myDS.connectionString"] = @"Server=.\sql2008r2;Database=ApiDataBase;uid=sa;pwd=111111;MultipleActiveResultSets=true";//连接字符串HangfireSettings.Instance.HangfireSqlserverConnectionString;//
                properties["quartz.dataSource.myDS.provider"] = "SqlServer";//数据库版本
                properties["quartz.scheduler.instanceId"] = "AUTO";
                //properties["quartz.jobStore.useProperties"] = "true";

                StdSchedulerFactory factory = new StdSchedulerFactory(properties);
                IScheduler scheduler = await factory.GetScheduler();

                if (!scheduler.IsStarted)
                {
                    await scheduler.Start();
                }
                // and start it off


                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(4)
                        .RepeatForever())
                    .Build();

                if (scheduler.CheckExists(new JobKey("job1", "group1")).Result)
                {
                    var sdsd = scheduler.DeleteJob(new JobKey("job1", "group1")).GetAwaiter().GetResult();
                }
                // Tell quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger);

                // some sleep to show what's happening
                await Task.Delay(TimeSpan.FromSeconds(60));

                // and last shut down the scheduler when you are ready to close your program
                await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        // simple log provider to get something to the console
        private class ConsoleLogProvider : ILogProvider
        {
            public Logger GetLogger(string name)
            {
                return (level, func, exception, parameters) =>
                {
                    if (level >= LogLevel.Info && func != null)
                    {
                        Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
                    }
                    return true;
                };
            }

            public IDisposable OpenNestedContext(string message)
            {
                throw new NotImplementedException();
            }

            public IDisposable OpenMappedContext(string key, string value)
            {
                throw new NotImplementedException();
            }
        }
    }

    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}
