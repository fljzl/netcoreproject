using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using apple.web.cms.quartz.Models;

namespace apple.web.cms.quartz.Controllers
{
    public class QuartzMangetController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public QuartzMangetController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
