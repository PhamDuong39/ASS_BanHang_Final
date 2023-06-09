using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Services;
using ASS_BanHang_Final.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASS_BanHang_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCartDetailsController : Controller
    {
        private readonly ICartDetailService _icartDetailService;
        public AdminCartDetailsController()
        {
            _icartDetailService = new CartDetailService();
        }
        public IActionResult Index()
        {
            var lst = _icartDetailService.GetAllCartDetail();
            return View(lst);
        }

        [HttpPost]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CartDetail obj)
        {
            _icartDetailService.CreateCartDetail(obj);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var obj = _icartDetailService.GetCartDetailById(id);
            return View(obj);
        }

        public IActionResult Delete(Guid id)
        {
            _icartDetailService.DeleteCartDetail(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var obj = _icartDetailService.GetCartDetailById(id);
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(CartDetail obj)
        {
            _icartDetailService.UpdateCartDetail(obj);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
