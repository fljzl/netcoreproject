using apple.test.BaseTest;
using System;
using System.Collections.Generic;
using System.Text;
using apple.Infrastructure;
using apple.data.Quartz;
using apple.Infrastructure.quartz;

namespace apple.test.quartz
{
    public class quartztest : ITest
    {
        public void Log()
        {
            //var manger = new QuatzjobRepostory();
            //var detail = manger.FindById(4);
            ////int totals = 0;
            ////var list = manger.GetListPage("", 1, 10, ref totals);
            //manger.UpdateBackgroundJobStatus(detail.JobId, 3);
            //detail = manger.FindById(4);
            //manger.UpdateBackgroundJobStatus(detail.JobId, DateTime.Now, default(DateTime));
        }

        public void Log(string str)
        {
            throw new NotImplementedException();
        }
    }
}
