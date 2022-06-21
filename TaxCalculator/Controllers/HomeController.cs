using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Models;

namespace TaxCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _applicationContext;
        public HomeController(ILogger<HomeController> logger,ApplicationContext databaseContext)
        {
            _logger = logger;
            _applicationContext = databaseContext;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }
        
        public IActionResult Index()
        {
            return View(_applicationContext.Thresholds.ToList());
        }
        
        
        public IActionResult Thresholds()
        {
            return View(_applicationContext.Thresholds.ToList());
        }
        
        [HttpGet]
        public IActionResult AddThreshold()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddThreshold(Threshold threshold)
        {
            _applicationContext.Thresholds.Add(threshold);
            await _applicationContext.SaveChangesAsync();
            return RedirectToAction("Thresholds");
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteThreshold(int id)
        {
            _applicationContext.Thresholds.Remove(_applicationContext.Thresholds.Find(id));
            await _applicationContext.SaveChangesAsync();
            return RedirectToAction("Thresholds");
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
