﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using apple.Infrastructure;
using apple.model.quarzt;
using Quartz;
namespace apple.core
{
    public class MangerQuartznet : IDisposable
    {
        private IScheduler Scheduler;
        public MangerQuartznet()
        {
            Scheduler = ManagerScheduler.Instance.Scheduler;
        }

        public bool DeleteJob(Customer_JobInfo jobInfo)
        {
            var jobKey = MangertKey.CreateJobKey(jobInfo.JobName, jobInfo.JobGroupName);
            var triggerKey = MangertKey.CreateTriggerKey(jobInfo.TriggerName, jobInfo.TriggerGroupName);
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
            var jobKey = MangertKey.CreateJobKey(jobInfo.JobName, jobInfo.JobGroupName);
            var triggerKey = MangertKey.CreateTriggerKey(jobInfo.TriggerName, jobInfo.TriggerGroupName);
            Scheduler.PauseTrigger(triggerKey);//.wait()
            Scheduler.PauseJob(jobKey);
            return true;
        }

        /// <summary>
        /// 重启任务，待续
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public bool ResumeJob(Customer_JobInfo jobInfo)
        {
            var jobKey = MangertKey.CreateJobKey(jobInfo.JobName, jobInfo.JobGroupName);
            var triggerKey = MangertKey.CreateTriggerKey(jobInfo.TriggerName, jobInfo.TriggerGroupName);
            Scheduler.ResumeJob(jobKey);
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
            var triggerKey = MangertKey.CreateTriggerKey(jobInfo.TriggerName, jobInfo.TriggerGroupName);
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
            var jobKey = MangertKey.CreateJobKey(jobInfo.JobName, jobInfo.JobGroupName);
            var flag = Scheduler.CheckExists(jobKey).Result;
            if (flag)
            {
                //存在job,先删除
                Scheduler.DeleteJob(jobKey).Wait();
            }

            if (!string.IsNullOrWhiteSpace(jobInfo.DLLName))
            {
                var jobdata = new JobDataMap() {
                        new KeyValuePair<string, object>(MangertKey.JobDataMapKeyJobId, jobInfo.JobId),
                        new KeyValuePair<string, object>("JobArgs", jobInfo.JobArgs),
                        new KeyValuePair<string, object>("RequestUrl", jobInfo.RequestUrl)
                   };

                var type = GetClassInfo(jobInfo.DLLName, jobInfo.FullJobName);
                IJobDetail jobDetail = JobBuilder.Create(type).WithIdentity(jobKey).UsingJobData(jobdata).RequestRecovery(true).Build();

                CronScheduleBuilder cronScheduleBuilder = CronScheduleBuilder.CronSchedule(jobInfo.Cron);
                ITrigger trigger = TriggerBuilder.Create()
                 .StartAt(DateTimeOffset.Now.AddYears(-1))
                 .WithIdentity(jobInfo.TriggerName, jobInfo.TriggerGroupName)
                 .ForJob(jobKey)
                 .WithSchedule(cronScheduleBuilder.WithMisfireHandlingInstructionDoNothing())
                 .Build();
                Scheduler.Start();
                Scheduler.ScheduleJob(jobDetail, trigger).Wait();
            }
            return true;
        }

        /// <summary>
        /// 反射获取对应的类型
        /// </summary>
        /// <param name="assemblyNamePath"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        private Type GetClassInfo(string assemblyNamePath, string className)
        {
            Type type = null;
            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyNamePath.Trim());
                type = assembly.GetType(className.Trim(), true, true);
            }
            catch (Exception ex)
            {
                Singleton<MangerLog>.Instance.Error("GetClassInfo:" + assemblyNamePath + className, ex);
            }
            return type;
        }

        /// <summary>
        /// 校验字符串是否为正确的Cron表达式
        /// </summary>
        /// <param name="cronExpression">带校验表达式</param>
        /// <returns></returns>
        public bool ValidExpression(string cronExpression)
        {
            return CronExpression.IsValidExpression(cronExpression);
        }
        public virtual void Dispose()
        {
            if (Scheduler != null && !Scheduler.IsShutdown)
            {
                Scheduler.Shutdown();
            }
        }
    }
}