using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace logtest
{
    public class Logmy
    {
        private readonly ILogger<Logmy> _logger;
        public Logmy(ILogger<Logmy> logger)
        {
            _logger = logger;
        }
        public void Index()
        {
            _logger.LogInformation("这是一个日志");

        }
    }

    public class HttpGlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;
        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {

            var actionName = context.HttpContext.Request.RouteValues["controller"] + "/" + context.HttpContext.Request.RouteValues["action"];
            _logger.LogError($"--------{actionName} Error Begin--------");
            _logger.LogError($"  Error Detail:" + context.Exception.Message);
            ////拦截处理
            //if (!context.ExceptionHandled)
            //{
            //    context.Result = new JsonResult(new TableData
            //    {
            //        status = false,
            //        msg = context.Exception.Message
            //    });
            //    context.ExceptionHandled = true;
            //}
            _logger.LogError($"--------{actionName} Error End--------");
        }
    }
}
