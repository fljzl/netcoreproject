using apple.data.baseDB;
using apple.model.quarzt;
using System;
using System.Collections.Generic;
using System.Text;

namespace apple.data.Quartz
{
    public class logMsgRepostory : SqlSugarDBContent
    {

        /// <summary>
        /// 新增job
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(logMsg model)
        {
            return logMsg.Insert(model);
        }

        /// <summary>
        /// 查询job详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public logMsg FindById(int JobId)
        {
            return logMsg.GetById(JobId);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="content"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<logMsg> GetListPage(string content, int pageIndex, int pageSize, ref int totals)
        {
            return Db.Queryable<logMsg>().Where(d => d.JobId.ToString().Contains(content) || d.JobName.Contains(content)).OrderBy(it => it.CreatTime, SqlSugar.OrderByType.Desc).ToPageList(pageIndex, pageSize, ref totals);
        }
    }
}
