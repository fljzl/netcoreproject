using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace apple.model.quarzt
{

 public class Customer_JobInfoViewModel: Customer_JobInfo
    {
        /// <summary>
        /// 持久化状态
        /// </summary>
        public string TRIGGER_STATE { get; set; }


        /// <summary>
        /// 下次执行时间
        /// </summary>
        public string NEXT_FIRE_TIME { get; set; }


        /// <summary>
        /// 上一次执行时间
        /// </summary>
        public string PREV_FIRE_TIME { get; set; }


        /// <summary>
        /// 本次调度开始时间
        /// </summary>
        public string START_TIME { get; set; }
    }
}
