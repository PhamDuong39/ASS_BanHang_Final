using ASS_BanHang_Final.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASS_BanHang_Final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["user"] = HttpContext.Session.GetString("username");
            ViewData["DiThi"] = HttpContext.Session.GetString("username");
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