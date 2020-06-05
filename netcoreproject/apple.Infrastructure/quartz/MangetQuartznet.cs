using System;
using System.Collections.Generic;
using System.Text;
using apple.model.quarzt;
using Quartz;
namespace apple.Infrastructure.quartz
{
    public class MangetQuartznet
    {
        private IScheduler Scheduler;
        public MangetQuartznet()
        {
            Scheduler = ManagerScheduler.Instance.Scheduler;
        }

        public bool DeleteJob(Customer_JobInfo jobInfo)
        {
            var jobKey = MangetKey.CreateJobKey(jobInfo.JobName, jobInfo.JobGroupName);
            var triggerKey = MangetKey.CreateTriggerKey(jobInfo.TriggerName, jobInfo.TriggerGroupName);
            Scheduler.PauseTrigger(triggerKey);
            Scheduler.UnscheduleJob(triggerKey);
            Scheduler.DeleteJob(jobKey);
            return true;
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public bool PauseJob(Customer_JobInfo jobInfo)
        {
            var jobKey = MangetKey.CreateJobKey(jobInfo.JobName, jobInfo.JobGroupName);
            Scheduler.PauseJob(jobKey);
            //jobinforrepository.UpdateBackgroundJobStatus(jobInfo.JobId, 5);
            return true;
        }

        /// <summary>
        /// 重启任务，待续
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public bool ResumeJob(Customer_JobInfo jobInfo)
        {
            var jobKey = MangetKey.CreateJobKey(jobInfo.JobName, jobInfo.JobGroupName);
            Scheduler.ResumeJob(jobKey);
            //jobinforrepository.UpdateBackgroundJobStatus(jobInfo.JobId, 3);
            return true;
        }

        /// <summary>
        /// 修改任务周期
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public bool ModifyJobCron(Customer_JobInfo jobInfo)
        {
            var scheduleBuilder = CronScheduleBuilder.CronSchedule(jobInfo.Cron);
            var triggerKey = MangetKey.CreateTriggerKey(jobInfo.TriggerName, jobInfo.TriggerGroupName);
            var trigger = TriggerBuilder.Create().StartAt(DateTimeOffset.Now.AddYears(-1)).WithIdentity(triggerKey).WithSchedule(scheduleBuilder.WithMisfireHandlingInstructionDoNothing()).Build();
            Scheduler.RescheduleJob(triggerKey, trigger);
            return true;
        }

        /// <summary>
        /// 运行job
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public bool RunJob(Customer_JobInfo jobInfo)
        {
            //var jobKey = MangetKey.CreateJobKey(jobInfo.JobName, jobInfo.JobGroupName);
            //var flag = !Scheduler.CheckExists(jobKey).Result;
            //if (flag)
            //{
            //    //通过dll运行
            //    if (!string.IsNullOrWhiteSpace(jobInfo.DLLName))
            //    {
            //        //var assemb = System.Reflection.Assembly.LoadFrom(jobInfo.DLLName.Trim());
            //        //var JobItem = assemb.GetType(jobInfo.FullJobName);

            //        var jobdata = new JobDataMap() {
            //            new KeyValuePair<string, object>(MangetKey.JobDataMapKeyJobId, jobInfo.JobId),
            //            new KeyValuePair<string, object>("JobArgs", jobInfo.JobArgs),
            //            new KeyValuePair<string, object>("RequestUrl", jobInfo.RequestUrl)
            //       };

            //        IJobDetail jobDetail = JobBuilder.Create<TestJob>().WithIdentity(jobKey).UsingJobData(jobdata).RequestRecovery(true).Build();

            //        CronScheduleBuilder cronScheduleBuilder = CronScheduleBuilder.CronSchedule(jobInfo.Cron);
            //        ITrigger trigger = TriggerBuilder.Create()
            //         .StartAt(DateTimeOffset.Now.AddYears(-1))
            //         .WithIdentity(jobInfo.TriggerName, jobInfo.TriggerGroupName)
            //         .ForJob(jobKey)
            //         .WithSchedule(cronScheduleBuilder.WithMisfireHandlingInstructionDoNothing())
            //         .Build();
            //        Scheduler.Start();
            //        Scheduler.ScheduleJob(jobDetail, trigger);
            //        //jobinforrepository.UpdateBackgroundJobStatus(jobInfo.JobId, 3);
            //    }
            //}
            return true;
        }

    }
}
