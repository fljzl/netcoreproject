using System;
using System.Threading.Tasks;
using Quartz;
namespace TestJob1
{
    public class TestJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine("TestJob" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            });
        }
    }
}
