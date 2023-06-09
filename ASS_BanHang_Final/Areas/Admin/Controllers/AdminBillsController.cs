using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace ASS_BanHang_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminBillsController : Controller
    {
        private readonly IBillService _ibillService;
        private readonly IUserService _iuserService;
        private readonly IBillDetailService _ibillDetailService;
        private readonly IProductService _iproductService;

        public AdminBillsController()
        {
            _ibillService = new BillService();
            _iuserService = new UserService();
            _ibillDetailService = new BillDetailService();
            _iproductService = new ProductService();
        }
        public IActionResult Index()
        {
            var lst = _ibillService.GetAllBill();
            
            List<User> lstUser = new List<User>();
            lstUser = _iuserService.GetAllUser();
            ViewData["GetUsername"] = new SelectList(lstUser, "Id", "Username");
            return View(lst);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(Bill obj)
        {
            _ibillService.CreateBill(obj);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            
            var billDetail = _ibillDetailService.GetBillDetailsByIdHD(id);

            List<Product> lstProduct = new List<Product>();
            lstProduct = _iproductService.GetAllProduct();

            ViewData["lstSlTProduct"] = new SelectList(lstProduct, "Id", "Name");
            return View(billDetail);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var obj = _ibillService.GetBillById(id);
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Bill obj)
        {
            _ibillService.UpdateBill(obj);
            return View();
        }

      


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
