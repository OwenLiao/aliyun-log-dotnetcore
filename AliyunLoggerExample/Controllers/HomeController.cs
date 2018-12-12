using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AliyunLoggerExample.Models;
using Microsoft.Extensions.Logging;

namespace AliyunLoggerExample.Controllers
{
    public class HomeController : Controller
    {
        private ILogger _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var start = DateTime.Now;
            _logger.LogInformation($"这是演示示例程序，看一下日志能否正确记录到阿里云，记录时间：{start}");
            var end = DateTime.Now;
            ViewBag.usedTime = (end - start).TotalMilliseconds;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
