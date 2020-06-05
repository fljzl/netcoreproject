using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
namespace apple.Infrastructure.quartz
{
    public class MangetKey
    {
        public static JobKey CreateJobKey(string jobName, string jobGroupName)
        {
            return new JobKey(jobName, jobGroupName);
        }
        public static TriggerKey CreateTriggerKey(string triggerName, string triggerGroupName)
        {
            return new TriggerKey(triggerName, triggerGroupName);
        }

        public static string JobDataMapKeyJobId = "JobId";
    }
}
