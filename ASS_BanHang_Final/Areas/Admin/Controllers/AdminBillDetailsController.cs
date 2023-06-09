using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Diagnostics;

namespace ASS_BanHang_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminBillDetailsController : Controller
    {
        private readonly IBillDetailService _ibillDetailService;
        public AdminBillDetailsController()
        {
            _ibillDetailService = new BillDetailService();
        }
        public IActionResult Index()
        {
            var lst = _ibillDetailService.GetAllBillDetail();
            return View(lst);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BillDetail obj)
        {
            _ibillDetailService.CreateBillDetail(obj);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var obj = _ibillDetailService.GetBillDetailById(id);
            return View(obj);
        }

        public IActionResult Delete(Guid id)
        {
            _ibillDetailService.DeleteBillDetail(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var obj = _ibillDetailService.GetBillDetailById(id);
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(BillDetail obj)
        {
            _ibillDetailService.UpdateBillDetail(obj);
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
