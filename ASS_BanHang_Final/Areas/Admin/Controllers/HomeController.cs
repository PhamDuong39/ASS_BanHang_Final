using Microsoft.AspNetCore.Mvc;

namespace ASS_BanHang_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["administrator"] = HttpContext.Session.GetString("admin");
            return View();
        }
    }
}
