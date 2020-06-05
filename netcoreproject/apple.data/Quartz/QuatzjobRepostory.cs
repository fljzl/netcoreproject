using apple.data.baseDB;
using apple.model.quarzt;
using System;
using System.Collections.Generic;
using System.Text;

namespace apple.data.Quartz
{
    public class QuatzjobRepostory : SqlSugarDBContent
    {

        /// <summary>
        /// 新增job
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(Customer_JobInfo model)
        {
            return Customer_JobInfo.Insert(model);
        }

        /// <summary>
        /// 查询job详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Customer_JobInfo FindById(int JobId)
        {

            return Customer_JobInfo.GetById(JobId);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="content"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Customer_JobInfo> GetListPage(string content, int pageIndex, int pageSize, ref int totals)
        {
            return Db.Queryable<Customer_JobInfo>().Where(d => d.JobId.ToString().Contains(content) || d.JobName.Contains(content)).OrderBy(it => it.JobId).ToPageList(pageIndex, pageSize, ref totals);
        }


        /// <summary>
        /// 更新Job运行信息 ,更改时间和运行次数
        /// </summary>
        /// <param name="JobId">Job ID</param>
        /// <param name="LastRunTime">最后运行时间</param>
        /// <param name="NextRunTime">下次运行时间</param>
        public bool UpdateBackgroundJobStatus(int JobId, DateTime? LastRunTime, DateTime? NextRunTime)
        {
            var t8 = Db.Updateable<Customer_JobInfo>().SetColumns(it => new Customer_JobInfo() { RunCount = it.RunCount + 1, NextTime = NextRunTime, PreTime = LastRunTime }).Where(it => it.JobId == JobId).ExecuteCommand();
            return t8 > 0;
        }

        /// <summary>
        /// 更新Job的状态
        /// </summary>
        /// <param name="JobId"></param>
        /// <param name="TriggerState"> 状态  0-停止  1-运行   3-正在启动中...   5-停止中...</param>
        /// <returns></returns>
        public bool UpdateBackgroundJobStatus(int JobId, int TriggerState)
        {

            return Db.Updateable(new Customer_JobInfo { JobId = JobId, TriggerState = TriggerState }).UpdateColumns(it => new { it.TriggerState }).ExecuteCommand() > 0;
        }


        /// <summary>
        /// 删除job
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        public bool DeleteBackgroundJob(int JobId)
        {
            return Db.Deleteable<Customer_JobInfo>().Where(new Customer_JobInfo() { JobId = JobId }).ExecuteCommand() > 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateCustomerJobInfo(Customer_JobInfo entity)
        {
            var t10 = Db.Updateable<Customer_JobInfo>().SetColumns(it => entity)
            .Where(it => it.JobId == entity.JobId).ExecuteCommand();
            return t10 > 0;
        }
    }
}
