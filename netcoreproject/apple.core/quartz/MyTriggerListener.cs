using apple.core;
using apple.data.Quartz;
using apple.Infrastructure;
using apple.model.quarzt;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskMangerData
{
    public class MyTriggerListener : ITriggerListener
    {
        logMsgRepostory _logmsg;
        public MyTriggerListener()
        {
            _logmsg = new logMsgRepostory();
        }

        public string Name { get; } = nameof(MyTriggerListener);

        public Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                var jobName = context.JobDetail.Key.Name;
                var msg = $"jobName:{jobName},Job: {context.JobDetail.Key} Name={Name},TriggerComplete";
                Singleton<MangerLog>.Instance.Error(msg);
                Console.WriteLine(msg);
                _logmsg.Insert(new logMsg
                {
                    JobName = jobName,
                    Msg = msg
                });
            });

        }

        public Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {

            return Task.Factory.StartNew(() =>
            {
                var jobName = context.JobDetail.Key.Name;
                var msg = $"jobName:{jobName},Job: {context.JobDetail.Key} Name={Name},TriggerFired";
                Singleton<MangerLog>.Instance.Error(msg);
                Console.WriteLine(msg);
                _logmsg.Insert(new logMsg
                {
                    JobName = jobName,
                    Msg = msg
                });
            });
        }

        public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                var jobName = trigger.JobDataMap;
                System.Text.StringBuilder log = new System.Text.StringBuilder();
                int i = 0;
                var joibId = 0;
                foreach (var item in trigger.JobDataMap)
                {
                    string key = item.Key;
                    if (key == MangertKey.JobDataMapKeyJobId)
                    {
                        joibId = Convert.ToInt32(item.Value);
                    }
                    if (key.StartsWith("extend_"))
                    {
                        if (i > 0)
                        {
                            log.Append(",");
                        }
                        log.AppendFormat("{0}:{1}", item.Key, item.Value);
                        i++;
                    }
                }

                var msg = $"错过触发时调用(例：线程不够用的情况下) joibId: {joibId} Name={Name},{log.ToString()},TriggerMisfired";
                //Singleton<MangerLog>.Instance.Error(msg);
                Console.WriteLine(msg);
                _logmsg.Insert(new logMsg
                {
                    JobName = joibId.ToString(),
                    Msg = msg
                });
            });
        }

        public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            //Trigger触发后，job执行时调用本方法。true即否决，job后面不执行。
            return Task.FromResult(false);   //返回true表示否决Job继续执行
        }

    }
}
