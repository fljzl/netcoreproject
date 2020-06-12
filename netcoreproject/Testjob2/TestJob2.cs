using Quartz;
using System;
using System.Threading.Tasks;

namespace Testjob2
{
    [DisallowConcurrentExecutionAttribute]
    [PersistJobDataAfterExecution]
    public class TestJob2 : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                JobKey key = context.JobDetail.Key;

                JobDataMap dataMap = context.MergedJobDataMap;  // Note the difference from the previous example

                //string jobSays = dataMap.GetString("jobSays");
                Console.WriteLine("key"+ key + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            });
        }
    }
}
