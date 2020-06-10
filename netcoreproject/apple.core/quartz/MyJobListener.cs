using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using apple.data.Quartz;
using apple.Infrastructure;

namespace apple.core
{
    public class MyJobListener : IJobListener
    {
        QuatzjobRepostory _quatzjobRepostory;
        logMsgRepostory _logmsg;
        public MyJobListener()
        {
            _quatzjobRepostory = new QuatzjobRepostory();
            _logmsg = new logMsgRepostory();
        }
        #region 监听
        public string Name { get; } = nameof(MyJobListener);

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            //Job即将执行
            return Task.Factory.StartNew(() =>
            {
                var jobName = context.JobDetail.Key.Name;
                var msg = $"jobName:{jobName},Job: {context.JobDetail.Key} 即将执行,Name={Name},JobToBeExecuted";
                Singleton<MangerLog>.Instance.Error(msg);
                Console.WriteLine(msg);
                _logmsg.Insert(new model.quarzt.logMsg
                {
                    JobName = jobName,
                    Msg = msg
                });
            });
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                var jobName = context.JobDetail.Key.Name;
                var msg = $"jobName:{jobName},Job: {context.JobDetail.Key} 被否决执行,Name={Name},JobExecutionVetoed";
                Singleton<MangerLog>.Instance.Error(msg);
                Console.WriteLine(msg);
                _logmsg.Insert(new model.quarzt.logMsg
                {
                    JobName = jobName,
                    Msg = msg
                });
            });
        }

        /// <summary>
        /// 运行成功以后
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobException"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            //Job执行完成
            return Task.Factory.StartNew(() =>
            {
                DateTime NextFireTimeUtc = context.Trigger.GetNextFireTimeUtc().HasValue ? context.Trigger.GetNextFireTimeUtc().Value.LocalDateTime : default(DateTime);
                DateTime FireTimeUtc = context.Trigger.GetPreviousFireTimeUtc().HasValue ? context.Trigger.GetPreviousFireTimeUtc().Value.LocalDateTime : default(DateTime);

                double TotalSeconds = context.JobRunTime.TotalSeconds;
                string JobName = context.JobDetail.Key.Name;
                string LogContent = $"jobName:{JobName},Job: {context.JobDetail.Key} 执行完成,Name={Name},JobWasExecuted";
                int joibId = 0;
                if (context.MergedJobDataMap != null)
                {
                    // JobName =  context.MergedJobDataMap.GetString("JobName");
                    System.Text.StringBuilder log = new System.Text.StringBuilder();
                    int i = 0;
                    foreach (var item in context.MergedJobDataMap)
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
                    if (i > 0)
                    {
                        LogContent = string.Concat("[", log.ToString(), "]");
                    }
                }
                if (jobException != null)
                {
                    LogContent = LogContent + " EX:" + jobException.ToString();
                }
                Singleton<MangerLog>.Instance.Infor(LogContent);
                Console.WriteLine(LogContent);
                _logmsg.Insert(new model.quarzt.logMsg
                {
                    JobName = JobName,
                    Msg = LogContent
                });
                _quatzjobRepostory.UpdateRunTime(joibId);
            });
        }
        #endregion


    }
}
