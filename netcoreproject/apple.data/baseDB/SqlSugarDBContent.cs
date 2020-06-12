using apple.Infrastructure;
using apple.model.quarzt;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace apple.data.baseDB
{
    public class SqlSugarDBContent
    {
        public SqlSugarClient Db;

        public SqlSugarDBContent()
        {
            Db = new SqlSugarClient(
                new ConnectionConfig
                {
                    ConnectionString = Singleton<MangerConfig>.Instance.sqlserverquartz,
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.SystemTable
                });

            //用来打印Sql方便你调式    
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            };
        }

        public DbSet<logMsg> logMsg { get { return new DbSet<logMsg>(Db); } }

        public DbSet<Customer_JobInfo> Customer_JobInfo { get { return new DbSet<Customer_JobInfo>(Db); } }

        public DbSet<QRTZ_BLOB_TRIGGERS> QRTZ_BLOB_TRIGGERS { get { return new DbSet<QRTZ_BLOB_TRIGGERS>(Db); } }
    }
    /// <summary>
    /// SimpleClient封装了单表大部分操作，此类为扩展类，以提供自定义的单表扩展方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        public DbSet(SqlSugarClient context) : base(context) { }
    }
}
