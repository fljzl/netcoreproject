using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskMangerData.Repository;

namespace TaskMangerData
{
    public class MyTriggerListener : ITriggerListener
    {
        private ILogCms logsnlog = LogCmsFactory.GetLogCms(0);
        JobInfoRepository jobinforrepository = new JobInfoRepository();
        LogMsgRepository logmsg = new LogMsgRepository();
        public string Name { get; } = nameof(MyTriggerListener);

        public Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {

            return Task.Factory.StartNew(() =>
            {
                logsnlog.Error($"Job: {context.JobDetail.Key} 执行完成");
                Console.WriteLine($"Job: {context.JobDetail.Key} 执行完成");

                var JobName = context.JobDetail.Key.Name;
                string LogContent = $"Job: {context.JobDetail.Key} 执行完成";
                int joibId = 0;
                if (context.MergedJobDataMap != null)
                {
                    // JobName =  context.MergedJobDataMap.GetString("JobName");
                    System.Text.StringBuilder log = new System.Text.StringBuilder();
                    int i = 0;
                    foreach (var item in context.MergedJobDataMap)
                    {
                        string key = item.Key;
                        if (key == JobCronTrigger.JobDataMapKeyJobId)
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
                    if (i > 0)
                    {
                        LogContent = string.Concat("[", log.ToString(), "]");
                    }
                }

                logmsg.Add(new LogMsgEntiry
                {
                    JobName = JobName,
                    CreatTime = DateTime.Now,
                    Msg = LogContent,
                    JobId = joibId
                });

            });

        }

        public Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {

            return Task.Factory.StartNew(() =>
            {
                logsnlog.Error($"Job: {context.JobDetail.Key} 运行中");
                Console.WriteLine($"Job: {context.JobDetail.Key} 运行中");

                var JobName = context.JobDetail.Key.Name;
                string LogContent = $"Job: {context.JobDetail.Key} 运行中";
                int joibId = 0;
                if (context.MergedJobDataMap != null)
                {
                    // JobName =  context.MergedJobDataMap.GetString("JobName");
                    System.Text.StringBuilder log = new System.Text.StringBuilder();
                    int i = 0;
                    foreach (var item in context.MergedJobDataMap)
                    {
                        string key = item.Key;
                        if (key == JobCronTrigger.JobDataMapKeyJobId)
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
                    if (i > 0)
                    {
                        LogContent = string.Concat("[", log.ToString(), "]");
                    }
                }

                logmsg.Add(new LogMsgEntiry
                {
                    JobName = JobName,
                    CreatTime = DateTime.Now,
                    Msg = LogContent,
                    JobId = joibId
                });

            });
        }

        public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                logsnlog.Error($"错过触发时调用(例：线程不够用的情况下)");
            });
        }

        public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            //Trigger触发后，job执行时调用本方法。true即否决，job后面不执行。
            return Task.FromResult(false);   //返回true表示否决Job继续执行
        }

    }
}
