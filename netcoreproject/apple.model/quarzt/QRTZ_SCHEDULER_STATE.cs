using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace apple.model.quarzt
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("QRTZ_SCHEDULER_STATE")]
    public partial class QRTZ_SCHEDULER_STATE
    {
           public QRTZ_SCHEDULER_STATE(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string SCHED_NAME {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string INSTANCE_NAME {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long LAST_CHECKIN_TIME {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long CHECKIN_INTERVAL {get;set;}

    }
}
