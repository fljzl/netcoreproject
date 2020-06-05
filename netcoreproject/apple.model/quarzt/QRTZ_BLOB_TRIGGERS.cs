using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace apple.model.quarzt
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("QRTZ_BLOB_TRIGGERS")]
    public partial class QRTZ_BLOB_TRIGGERS
    {
           public QRTZ_BLOB_TRIGGERS(){


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
           public string TRIGGER_NAME {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string TRIGGER_GROUP {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte[] BLOB_DATA {get;set;}

    }
}
