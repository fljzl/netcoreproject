using Quartz;
using System;
using System.Threading.Tasks;

namespace Testjob2
{
    public class TestJob2 : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine("TestJob2" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            });
        }
    }
}
