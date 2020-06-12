using apple.Infrastructure;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using TaskMangerData;

namespace apple.core
{
    public class SchedulerManger
    {
        private IScheduler _scheduler;

        bool _isweb = true;
        public SchedulerManger()
        {
            Scheduler = _getScheduler().Result;
        }
        public SchedulerManger(bool isweb = true)
        {
            _isweb = isweb;
            Scheduler = _getScheduler().Result;
        }
        public IScheduler Scheduler { get => _scheduler; set => _scheduler = value; }

        private async Task<IScheduler> _getScheduler()
        {
            MangerConfig configs;
            if (_isweb)
            {
                configs = Singleton<MangerConfig>.Instance;
            }
            else
            {
                configs = new MangerConfig(_isweb);
            }
            var listconfig = configs.quartzlist;
            var properties = new NameValueCollection();
            //foreach (var item in listconfig)
            //{
            //    properties.Add(item.name, item.value);
            //}

            //properties["quartz.scheduler.proxy"] = "true";
            //properties["quartz.scheduler.proxy.address"] = $"{configs.quartzchannelType}://{configs.quartzlocalIp}:{configs.quartzport}/{configs.quartzbindName}";
            //properties["quartz.scheduler.instanceName"] = "RemoteServer";
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "500";
            properties["quartz.threadPool.threadPriority"] = "Normal";
            //properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            //properties["quartz.scheduler.exporter.port"] = "555";//端口号
            //properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";//名称
            //properties["quartz.scheduler.exporter.channelType"] = "tcp";//通道类型
            //properties["quartz.scheduler.exporter.channelName"] = "httpQuartz";
            //properties["quartz.scheduler.exporter.rejectRemoteRequests"] = "true";


            properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";//存储类型
            properties["quartz.serializer.type"] = "json";
            properties["quartz.jobStore.tablePrefix"] = "Qrtz_";//表名前缀
            properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";//驱动类型
            properties["quartz.jobStore.dataSource"] = "myDS";//数据源名称
            properties["quartz.dataSource.myDS.connectionString"] = @"Server=.\sql2008r2;Database=ApiDataBase;uid=sa;pwd=111111;MultipleActiveResultSets=true";//连接字符串HangfireSettings.Instance.HangfireSqlserverConnectionString;//

            //集群配置
            properties["quartz.scheduler.instanceId"] = "AUTO";
            properties["quartz.jobStore.clustered"] = "true";
            properties["quartz.jobStore.useProperties"] = "true";


            //if (configs.IsUseproxy)
            //{
            //    var address = $"{configs.quartzchannelType}://{configs.localIp}:{configs.quartzport}/{configs.quartzbindName}";
            //    properties.Add("quartz.scheduler.proxy", "true");
            //    properties.Add("quartz.scheduler.proxy.address", address);
            //}

            var schedulerFactory = new StdSchedulerFactory(properties);

            Scheduler = await schedulerFactory.GetScheduler();
            await Scheduler.Start();

            //Scheduler = schedulerFactory.GetScheduler().Result;

            Scheduler.ListenerManager.AddJobListener(new MyJobListener(), GroupMatcher<JobKey>.AnyGroup());

            //Scheduler.ListenerManager.AddSchedulerListener(new MySchedulerListener());

            //Scheduler.ListenerManager.AddTriggerListener(new MyTriggerListener(), GroupMatcher<TriggerKey>.AnyGroup());
            return Scheduler;
        }

    }
}
