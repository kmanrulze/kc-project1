using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreApp.WebApp.Models;
using static StoreApp.BusinessLogic.Objects.GuidService;

namespace StoreApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SingletonGuidService singletonGUID;

        private readonly ScopedGuidService scopedGUID;

        private readonly TransientGuidService transientGUID;



        public HomeController(ILogger<HomeController> logger, SingletonGuidService singleton, ScopedGuidService scoped, TransientGuidService transient)
        {
            _logger = logger;
            singletonGUID = singleton;
            scopedGUID = scoped;
            transientGUID = transient;
        }

        public IActionResult Index([FromServices] SingletonGuidService singleton, [FromServices] ScopedGuidService scoped, [FromServices] TransientGuidService transient)
        {
            /*
            ViewData["singleton"] = singletonGUID;
            ViewData["scoped"] = scopedGUID;
            ViewData["transient"] = transientGUID;
            */

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
