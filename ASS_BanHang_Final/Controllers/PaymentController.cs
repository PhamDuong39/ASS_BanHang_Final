using ASS_BanHang_Final.Extensions;
using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.Services;
using ASS_BanHang_Final.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace ASS_BanHang_Final.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IBillService _ibillService;
        private readonly IBillDetailService _ibillDTService;
        private readonly IProductService _iproductService;
        private readonly IUserService _iuserService;

        public PaymentController(IBillService billService, IBillDetailService billDetailService, IProductService productService, IUserService userService)
        {
            _ibillDTService = billDetailService;
            _ibillService = billService;
            _iproductService = productService;
            _iuserService = userService;
        }

        public IActionResult Index()
        {

            return View();
        }
        private Guid UserID;
        public IActionResult ViewPayment()
        {
            ViewData["user"] = HttpContext.Session.GetString("username");
            string user = ViewData["user"] == null ? "" : ViewData["user"] as string;

            if (user == "")
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                UserID = _iuserService.GetUserIdByUsername(user);
                List<CartDetail> cart = HttpContext.Session.GetJson<List<CartDetail>>("Cart") ?? new List<CartDetail>();

                // Khoi tao CartDTVM (list<cart>, Tong so tien)
                CartDetailViewModel cartDetailVM = new CartDetailViewModel()
                {
                    cartItems = cart,
                    GrandTotal = cart.Sum(p => p.Quantity * _iproductService.GetProductById(p.IdSp).Price)
                };

                ViewData["ConfirmPaymentView"] = cartDetailVM;

                List<Product> lstProduct = new List<Product>();
                lstProduct = _iproductService.GetAllProduct();

                ViewData["lstSlTProduct"] = new SelectList(lstProduct, "Id", "Name");


                return View();
            }          
        }


        Guid IdHD = Guid.NewGuid();
        [HttpPost]
        public IActionResult RegisterTheCart(Bill billCreate)
        {
            ViewData["user"] = HttpContext.Session.GetString("username");
            string user = ViewData["user"] == null ? "" : ViewData["user"] as string;


            if (user == "")
            {
                return RedirectToAction("Login", "Account");
            }
            //Guid userId = _iuserService.GetAllUser().FirstOrDefault(p => p.Username == user).Id;

            string abc = HttpContext.Session.GetString("UserID");
            // string abc OK

            //
            var test =  Guid.Parse(abc);
            // Loi o day
            var userID = _iuserService.GetUserIdByUsername(user);
            

            Bill bill = new Bill()
            {
                Id = IdHD,
                CreateDate = DateTime.Now,
                UserID = test,
                Status = 1,
                Address = billCreate.Address,
                SDT = billCreate.SDT
                // Status = 1 : Cho xac nhan cua Admin
                // Status = 2 : Don hang da duoc van chuyen
                // Status = 3 : Don hang da duoc van chuyen thanh cong
            };

            _ibillService.CreateBill(bill);

            this.CreateBillDT();
            return RedirectToAction("ShowBillHistory", "BillHistory");
        }


        [HttpPost]
        public IActionResult CreateBillDT()
        {
            
            List<CartDetail> cart = HttpContext.Session.GetJson<List<CartDetail>>("Cart") ?? new List<CartDetail>();

            CartDetailViewModel cartDetailVM = new CartDetailViewModel()
            {
                cartItems = cart,
                GrandTotal = cart.Sum(p => p.Quantity * _iproductService.GetProductById(p.IdSp).Price)
            };

            foreach (var item in cartDetailVM.cartItems)
            {
                BillDetail billDetail = new BillDetail()
                {
                    Id = Guid.NewGuid(),
                    IdHD = IdHD,
                    IdSP = item.IdSp,
                    Quantity = item.Quantity,
                    Price = item.Price,
                };

                var oldProduct = _iproductService.GetProductById(item.IdSp);
                Product product = new Product()
                {
                    Id = item.IdSp,
                    Name = oldProduct.Name,
                    Price = oldProduct.Price,
                    AvailableQuantity = oldProduct.AvailableQuantity - item.Quantity,
                    Status = oldProduct.Status,
                    Supplier = oldProduct.Supplier,
                    Description = oldProduct.Description,
                };

                _ibillDTService.CreateBillDetail(billDetail);
                _iproductService.UpdateProduct(product);
            }
            HttpContext.Session.Remove("Cart");
            return Content("Success");
        }

        
    }
}
