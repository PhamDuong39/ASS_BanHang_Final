using ASS_BanHang_Final.Extensions;
using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASS_BanHang_Final.Controllers
{
    public class ProductController : Controller
    {
        
        private readonly IProductService _iproductService;
        public static int SoLuongThemVaoGiohang;

        public ProductController(IProductService productService)
        {
            
            //_iproductService = new ProductService();
            // Su dung DI 
            _iproductService = productService;
        }

        public IActionResult Index(string SearchStringName)
        {
            List<Product> lstprd = new List<Product>();
            lstprd = _iproductService.GetAllProduct();

            if(!string.IsNullOrEmpty(SearchStringName))
            {
                lstprd = lstprd.Where(p => p.Name.ToLower().Contains(SearchStringName.ToLower())).ToList();
            }

            ViewData["user"] = HttpContext.Session.GetString("username");
            return View(lstprd);
        }

        [HttpGet]
        public IActionResult Details(Guid Id)
        {
            var obj = _iproductService.GetProductById(Id);
           
            return View(obj);
        }

        [HttpPost]
        public IActionResult AddProductByQuantity(Guid IdSP)
        {
            // Tim product muon them vao gio hang
            Product product = _iproductService.GetProductById(IdSP);

            // ktra xem gio hang da co SP nao hay chua. Co roi thi lay ra, Chua co thi khoi tao 1 list rong de ko bi loi
            List<CartDetail> cart = HttpContext.Session.GetJson<List<CartDetail>>("Cart") ?? new List<CartDetail>();

            // Ktra xem gio hang da co san pham muon them o dong dau hay chua
            CartDetail cartItem = cart.Where(p => p.IdSp == IdSP).FirstOrDefault();


            if (cartItem == null)
            {
                //int SL = Convert.ToInt32(SoLuong);

                // Neu chua co thi tao 1 CartDetail moi tu ctor o Class Model Cartdetail
                cart.Add(new CartDetail(product));
            }
            else
            {
                //int SL = Convert.ToInt32(SoLuong);
                // Neu co roi thi So luong san pham ++ 1
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);
            TempData["Success"] = "The product has been added";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(string soLuong)
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
