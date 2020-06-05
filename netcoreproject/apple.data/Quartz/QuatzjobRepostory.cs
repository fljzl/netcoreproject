using apple.data.baseDB;
using apple.model.quarzt;
using System;
using System.Collections.Generic;
using System.Text;

namespace apple.data.Quartz
{
    public class QuatzjobRepostory : SqlSugarDBContent
    {
        public List<Customer_JobInfo> GetList()
        {
            return Customer_JobInfo.GetList();
        }
    }
}
