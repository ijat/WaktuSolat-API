using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WaktuSolat_API.Models;
using WaktuSolat_API.Services;

namespace WaktuSolat_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private WaktuSolatService _waktuSolatService;

        public HomeController(ILogger<HomeController> logger, WaktuSolatService waktuSolatService)
        {
            _waktuSolatService = waktuSolatService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [HttpGet("{kod}")]
        public Task<WaktuSolat> GetWaktuSolat(string kod)
        {
            WaktuSolat ws = _waktuSolatService.GetWaktuSolat(kod);
            return Task.FromResult(ws);
        }
    }
}