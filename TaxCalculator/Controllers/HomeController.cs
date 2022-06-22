using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Models;
using TaxCalculator.Services;

namespace TaxCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _context;
        private TaxService _taxService;
        public HomeController(ApplicationContext context )
        {
            _context = context;
            _taxService = new TaxService(context);;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }
        
        public IActionResult Index()
        {
            return View(_context.Thresholds.ToList());
        }

        [HttpPost]
        public async Task<string> CalculateTax(int income)
        {
            double result = _taxService.CalculateTax(income);
            return String.Format($"Your taxes is: {result:f2} $ which is" +
                                 $": {result/income*100:f1} % of your income.") ;
        }
        
        public IActionResult Thresholds()
        {
            return View(_context.Thresholds.ToList());
        }
        
        [HttpGet]
        public IActionResult AddThreshold()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddThreshold(Threshold threshold)
        {
            _context.Thresholds.Add(threshold);
            await _context.SaveChangesAsync();
            return RedirectToAction("Thresholds");
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteThreshold(int id)
        {
            _context.Thresholds.Remove(_context.Thresholds.Find(id));
            await _context.SaveChangesAsync();
            return RedirectToAction("Thresholds");
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
