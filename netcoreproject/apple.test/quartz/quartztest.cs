using apple.test.BaseTest;
using System;
using System.Collections.Generic;
using System.Text;
using apple.Infrastructure;
using apple.data.Quartz;

namespace apple.test.quartz
{
    public class quartztest : ITest
    {
        public void Log()
        {
            var list = new QuatzjobRepostory().GetList();
            foreach (var item in list)
            {
                Console.WriteLine(item.FullJobName);
            }
        }

        public void Log(string str)
        {
            throw new NotImplementedException();
        }
    }
}
