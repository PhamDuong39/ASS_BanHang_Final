using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASS_BanHang_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCartsController : Controller
    {
        private readonly ICartService _icartService;
        public AdminCartsController()
        {
            _icartService = new CartService();
        }

        public IActionResult Index()
        {
            var lst = _icartService.GetAllCarts();
            return View(lst);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Cart obj)
        {
            _icartService.CreateCart(obj);
            return RedirectToAction("Index");   
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var obj = _icartService.GetCartById(id);
            return View(obj);
        }

        public IActionResult Delete(Guid id)
        {
            _icartService.DeleteCart(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var obj = _icartService.GetCartById(id);
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Cart obj)
        {
            _icartService.CreateCart(obj);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
