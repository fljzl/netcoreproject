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
    public class LogMangertController : Controller
    {
        logMsgRepostory _logmsg;
        public LogMangertController()
        {
            _logmsg = new logMsgRepostory();
        }

        public IActionResult Index()
        {
            return View();
        }

        public string GetCouponIndexData(string content = "", int page = 1, int limit = 50)
        {
            var total = 0;
            var result = _logmsg.GetListPage(content, page, limit, ref total);
            return JsonConvert.SerializeObject(new ResultMsg<IEnumerable<logMsg>>(0, "", result, total));
        }
    }
}
