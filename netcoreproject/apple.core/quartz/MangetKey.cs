using System;
using System.Collections.Generic;
using System.Text;
using apple.model.quarzt;
using Quartz;
namespace apple.core
{

    public class MangertKey
    {
        public static JobKey CreateJobKey(string jobName, string jobGroupName)
        {
            return new JobKey(jobName, jobGroupName);
        }
        public static TriggerKey CreateTriggerKey(string triggerName, string triggerGroupName)
        {
            return new TriggerKey(triggerName, triggerGroupName);
        }



        private ITrigger CreateTrigger(Customer_JobInfo jobInfo)
        {
            TriggerBuilder tiggerBuilder = TriggerBuilder.Create().WithIdentity(jobInfo.JobName, jobInfo.JobGroupName);

            //错过的不管了，剩下的按正常执行。
            //tiggerBuilder.WithCronSchedule(jobInfo.Cron, c => c.WithMisfireHandlingInstructionDoNothing());

            ////错过的合并为一次执行，后续正常执行。
            tiggerBuilder.WithCronSchedule(jobInfo.Cron, c => c.WithMisfireHandlingInstructionFireAndProceed());

            ////错过的马上执行掉，后续正常执行
            //tiggerBuilder.WithCronSchedule(jobInfo.Cron, c => c.WithMisfireHandlingInstructionIgnoreMisfires());


            //if (jobInfo.StartTime > DateTime.Now)
            //{
            //    tiggerBuilder.StartAt(jobInfo.StartTime);
            //}
            //else
            //{
            //    tiggerBuilder.StartNow();
            //}

            ITrigger trigger = tiggerBuilder.Build();
            return trigger;
        }
        public static string JobDataMapKeyJobId = "JobId";
    }
}
