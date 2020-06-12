using apple.core;
using apple.Infrastructure;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using Topshelf;
namespace TopshelfWindow
{
    public class TraderService : ServiceControl
    {
        MangerLog _logs;
        IScheduler _scheduler;
        public TraderService()
        {
            _logs = Singleton<MangerLog>.Instance;
            _scheduler = new ManagerScheduler(false).Scheduler;
        }

        public bool Start(HostControl hostControl)
        {
            _logs.Infor("任务Start");
            try
            {
                _scheduler.Start().Wait();
            }
            catch (Exception ex)
            {

                _logs.Infor("任务Start", ex);
            }

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _logs.Infor("任务Stop");
            _scheduler.Shutdown(true).Wait();
            return true;
        }
    }



}
