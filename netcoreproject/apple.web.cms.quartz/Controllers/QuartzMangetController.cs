using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using apple.web.cms.quartz.Models;
using apple.data.Quartz;
using apple.model.quarzt;
using apple.model;
using Newtonsoft.Json;
using apple.core;

namespace apple.web.cms.quartz.Controllers
{
    public class QuartzMangetController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        QuatzjobRepostory _quartzrepository;
        MangerQuartznet _quartzmanger;
        public QuartzMangetController(ILogger<HomeController> logger)
        {
            _quartzrepository = new QuatzjobRepostory();
            _quartzmanger = new MangerQuartznet();
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string GetCouponIndexData(string content = "", int page = 1, int limit = 50)
        {
            //需要修改
            var total = 0;
            var result = _quartzrepository.GetListPage(content, page, limit, ref total);
            return JsonConvert.SerializeObject(new ResultMsg<IEnumerable<Customer_JobInfoViewModel>>(0, "", result, total));
        }

        #region 操作任务

        public ActionResult SetIndex(int JobId)
        {
            ViewBag.modeldata = _quartzrepository.FindById(JobId);
            return View();
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="addJobViewModel">添加任务模型</param>
        /// <returns></returns>
        [HttpPost]

        public ActionResult AddJob(Customer_JobInfo obj)
        {
            if (obj.JobId > 0)
            {
                //修改
                var jobId = _quartzrepository.UpdateCustomerJobInfo(obj);

                if (jobId)
                {
                    var result = new ExecutionResult() { IsSuccess = true, Message = "成功" };
                    return Json(result);

                }
                else
                {
                    var result = new ExecutionResult() { IsSuccess = false, Message = "失败" };
                    return Json(result);
                }
            }
            else
            {
                var jobId = _quartzrepository.Insert(obj);

                if (jobId)
                {
                    var result = new ExecutionResult() { IsSuccess = true, Message = "成功" };
                    return Json(result);

                }
                else
                {
                    var result = new ExecutionResult() { IsSuccess = false, Message = "失败" };
                    return Json(result);
                }
            }
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="jobId">任务编号</param>
        /// <returns></returns>
        [HttpPost]

        public JsonResult RunJob(int JobId)
        {
            var jobDetail = _operateJob(JobId);
            if (jobDetail != null)
            {
                var flag = _quartzmanger.RunJob(jobDetail);
                return Json(new ExecutionResult() { IsSuccess = flag, Message = flag ? "成功" : "失败" });
            }
            else
            {
                return Json(new ExecutionResult() { IsSuccess = false, Message = "失败" });
            }
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="jobId">任务编号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteJob(int JobId)
        {
            var jobDetail = _operateJob(JobId);
            if (jobDetail != null)
            {
                var flag = _quartzmanger.DeleteJob(jobDetail);
                return Json(new ExecutionResult() { IsSuccess = flag, Message = flag ? "成功" : "失败" });
            }
            else
            {
                return Json(new ExecutionResult() { IsSuccess = false, Message = "失败" });
            }
        }
        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="jobId">任务编号</param>
        /// <returns></returns>
        [HttpPost]

        public JsonResult Pause(int JobId)
        {
            var jobDetail = _operateJob(JobId);
            if (jobDetail != null)
            {
                var flag = _quartzmanger.PauseJob(jobDetail);
                return Json(new ExecutionResult() { IsSuccess = flag, Message = flag ? "成功" : "失败" });
            }
            return Json(new ExecutionResult() { IsSuccess = false, Message = "失败" });
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        public JsonResult Remove(int JobId)
        {
            var jobDetail = _operateJob(JobId);
            if (jobDetail != null)
            {
                var flag = _quartzmanger.PauseJob(jobDetail);
                return Json(new ExecutionResult() { IsSuccess = flag, Message = flag ? "成功" : "失败" });
            }
            return Json(new ExecutionResult() { IsSuccess = false, Message = "失败" });
        }

        /// <summary>
        /// 恢复任务
        /// </summary>
        /// <param name="jobId">任务编号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Resume(int JobId)
        {
            var jobDetail = _operateJob(JobId);
            if (jobDetail != null)
            {
                var flag = _quartzmanger.ResumeJob(jobDetail);
                return Json(new ExecutionResult() { IsSuccess = flag, Message = flag ? "成功" : "失败" });
            }
            return Json(new ExecutionResult() { IsSuccess = false, Message = "失败" });
        }




        /// <summary>
        /// 操作任务
        /// </summary>
        /// <param name="jobId">任务编号</param>
        /// <param name="operateJobFunc">具体操作任务的委托</param>
        /// <returns></returns>
        private Customer_JobInfo _operateJob(int JobId)
        {
            return _quartzrepository.FindById(JobId);
        }


        #endregion
    }
}
