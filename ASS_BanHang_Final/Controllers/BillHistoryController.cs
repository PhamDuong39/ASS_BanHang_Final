using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.Services;
using ASS_BanHang_Final.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASS_BanHang_Final.Controllers
{
    public class BillHistoryController : Controller
    {
        private readonly IBillService _ibillService;
        private readonly IBillDetailService _ibillDTService;
        private readonly IUserService _iuserService;
        private readonly IProductService _iproductService;

        public BillHistoryController(IBillService billService, IBillDetailService billDetailService, IUserService userService, IProductService productService)
        {
            _ibillDTService = billDetailService;
            _ibillService = billService;
            _iuserService = userService;
            _iproductService = productService;
        }

        public IActionResult ShowBillHistory()
        {
            ViewData["user"] = HttpContext.Session.GetString("username");
            string user = ViewData["user"] == null ? "" : ViewData["user"] as string;
            if (user == null)
            {
                return Content("Error");
            }

            List<Product> lstProduct = new List<Product>();
            lstProduct = _iproductService.GetAllProduct();

            ViewData["lstSlTProduct"] = new SelectList(lstProduct, "Id", "Name");
            //Guid userId = _iuserService.GetUserIdByUsername(user);
            //var allIdBillByUser = _ibillService.GetAllBillByUserId(userId).Select(p => p.Id);
            //var allBillByUser = _ibillService.GetAllBillByUserId(Guid.Parse("C93669B5-F21F-423B-8811-83C6ADAEB44A"));
            //var allBillDeatail = _ibillDTService.GetAllBillDetail();

            //var test = _ibillService.GetAllBillHistory(Guid.Parse("C93669B5-F21F-423B-8811-83C6ADAEB44A"));
            var userID = _iuserService.GetUserIdByUsername(user);
            var test = _ibillService.GetAllBillHistory(userID);

            return View(test);
            //return View(listBillHistory);
        }
    }
}
