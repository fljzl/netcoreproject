using Quartz;
using System;
using System.Threading.Tasks;

namespace Testjob2
{
    [DisallowConcurrentExecutionAttribute]
    public class TestJob2 : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine("TestJob3" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            });
        }
    }
}
