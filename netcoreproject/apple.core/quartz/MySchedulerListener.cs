using apple.data.Quartz;
using apple.Infrastructure;
using apple.model.quarzt;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace TaskMangerData
{
    public class MySchedulerListener : ISchedulerListener
    {

        logMsgRepostory _logmsg;
        public MySchedulerListener()
        {
            _logmsg = new logMsgRepostory();
        }

        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                var jobName = jobDetail.Key.Name;
                var msg = $"jobName:{jobName},Job: {jobDetail.Key} Name=MySchedulerListener,JobAdded";
                Singleton<MangerLog>.Instance.Error(msg);
                Console.WriteLine(msg);
                _logmsg.Insert(new logMsg
                {
                    JobName = jobName,
                    Msg = msg
                });
            });
        }

        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"JobDeleted");
            });
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"JobInterrupted");
            });
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"JobPaused");
            });
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"JobResumed");
            });
        }

        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"JobScheduled");
            });
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"JobScheduled");
            });
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"JobsResumed");
            });
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"JobUnscheduled");
            });
        }

        public Task SchedulerError(string msg, SchedulerException SchedulerError, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                var msglost = msg + SchedulerError.Message + SchedulerError.StackTrace;
                foreach (var item in SchedulerError.Data)
                {
                    msglost += item.ToString();
                }
                Singleton<MangerLog>.Instance.Error(msglost);
                Console.WriteLine(msg);
                _logmsg.Insert(new logMsg
                {
                    JobName = "SchedulerError",
                    Msg = msglost
                });
            });
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {

            });
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {

            });
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {

            });
        }

        public Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() => { });
        }

        public Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() => { });
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() => { });
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"TriggerFinalized");
            });
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"TriggerPaused");
            });
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"TriggerResumed");
            });
        }

        public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"TriggersPaused");
            });
        }

        public Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"TriggersResumed");
            });
        }
    }
}
