using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace apple.model.quarzt
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("logMsg")]
    public partial class logMsg
    {
           public logMsg(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Msg {get;set;}

           /// <summary>
           /// Desc:
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           public DateTime? CreatTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JobName {get;set;}


    }
}
