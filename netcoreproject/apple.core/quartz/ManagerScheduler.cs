using apple.Infrastructure;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using TaskMangerData;

namespace apple.core
{
    public class ManagerScheduler: Singleton<ManagerScheduler>
    {
        private IScheduler _scheduler;

        public ManagerScheduler()
        {
            Scheduler = _getScheduler();
        }

        public IScheduler Scheduler { get => _scheduler; set => _scheduler = value; }

        private  IScheduler _getScheduler()
        {
            var listconfig = MangerConfig.quartzlist;
            var properties = new NameValueCollection();
            foreach (var item in listconfig)
            {
                properties.Add(item.name, item.value);
            }

            if (MangerConfig.IsUseproxy)
            {
                var address = $"{MangerConfig.quartzchannelType}://{MangerConfig.localIp}:{MangerConfig.quartzport}/{MangerConfig.quartzbindName}";
                properties.Add("quartz.scheduler.proxy", "true");
                properties.Add("quartz.scheduler.proxy.address", address);
            }

            var schedulerFactory = new StdSchedulerFactory(properties);
           
            Scheduler = schedulerFactory.GetScheduler().Result;

            Scheduler.ListenerManager.AddJobListener(new MyJobListener(), GroupMatcher<JobKey>.AnyGroup());

            Scheduler.ListenerManager.AddSchedulerListener(new MySchedulerListener());

            Scheduler.ListenerManager.AddTriggerListener(new MyTriggerListener(), GroupMatcher<TriggerKey>.AnyGroup());
            return Scheduler;
        }

    }
}
