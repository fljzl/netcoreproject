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
        public List<Customer_JobInfoViewModel> GetListPage(string content, int pageIndex, int pageSize, ref int totals)
        {

            List<Customer_JobInfoViewModel> list = new List<Customer_JobInfoViewModel>();

            var sqlBuilder = "SELECT   tab1.JobId, tab1.JobGroupName, tab1.JobName, tab1.TriggerName, tab1.Cron, tab1.Description, tab1.CreateTime, tab1.TriggerGroupName, tab1.DLLName, tab1.FullJobName, tab1.Deleted, tab1.RequestUrl, tab1.Createtor, tab1.UpdateTime, tab1.Updator, tab1.RunCount, tab1.CronExpressionDescription, tab1.JobArgs,b.TRIGGER_STATE,b.NEXT_FIRE_TIME,b.PREV_FIRE_TIME,b.START_TIME FROM dbo.Customer_JobInfo tab1 LEFT JOIN dbo.QRTZ_TRIGGERS b ON tab1.JobName = b.JOB_NAME ";
            list = Db.SqlQueryable<Customer_JobInfoViewModel>(sqlBuilder).ToList();
            list.ForEach(f =>
            {
                f.NEXT_FIRE_TIME = string.IsNullOrWhiteSpace(f.NEXT_FIRE_TIME) ? "" : new DateTime(Convert.ToInt64(f.NEXT_FIRE_TIME)).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
                f.PREV_FIRE_TIME = string.IsNullOrWhiteSpace(f.PREV_FIRE_TIME) ? "" : new DateTime(Convert.ToInt64(f.PREV_FIRE_TIME)).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
                f.START_TIME = string.IsNullOrWhiteSpace(f.START_TIME) ? "" : new DateTime(Convert.ToInt64(f.START_TIME)).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
            });
            return list;
            //return Db.Queryable<Customer_JobInfo>().Where(d => d.JobId.ToString().Contains(content) || d.JobName.Contains(content)).OrderBy(it => it.JobId).ToPageList(pageIndex, pageSize, ref totals);
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

        public bool UpdateRunTime(int JobId)
        {
            var t8 = Db.Updateable<Customer_JobInfo>().SetColumns(it => new Customer_JobInfo() { RunCount = it.RunCount + 1 }).Where(it => it.JobId == JobId).ExecuteCommand();
            return t8 > 0;
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
