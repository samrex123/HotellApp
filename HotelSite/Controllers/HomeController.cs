using HotelSite.Data;
using HotelSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        //private readonly HotellAppContext _db;

        public HomeController(ILogger<HomeController> logger,
                                ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _db = applicationDbContext;
        }

        public IActionResult Index()
        {
            //Exempeltest
            //var x = _db.Customers.FirstOrDefault(myvar => myvar.Country == "Canada");
            //if (x == null)
            //    throw new Exception("No Canadian customers!");
            //return View(x);

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