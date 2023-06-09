using ASS_BanHang_Final.Extensions;
using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.Services;
using ASS_BanHang_Final.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace ASS_BanHang_Final.Controllers
{
    public class CartController : Controller
    {
        
        private readonly IProductService _iproductService;
        public CartController(IProductService productService)
        {
            
            _iproductService = productService;
        }
        public IActionResult Index()
        {
            ViewData["user"] = HttpContext.Session.GetString("username");
            // ktra xem gio hang da co SP nao hay chua. Co roi thi lay ra, Chua co thi khoi tao 1 list rong de ko bi loi
            List<CartDetail> cart = HttpContext.Session.GetJson<List<CartDetail>>("Cart") ?? new List<CartDetail>();

            // Khoi tao CartDTVM (list<cart>, Tong so tien)
            CartDetailViewModel cartDetailVM = new CartDetailViewModel() 
            {
                cartItems = cart,
                GrandTotal = cart.Sum(p => p.Quantity *  _iproductService.GetProductById(p.IdSp).Price )
            };

            List<Product> lstProduct = new List<Product>();
            lstProduct = _iproductService.GetAllProduct();

            ViewData["lstSlTProduct"] = new SelectList(lstProduct, "Id", "Name");

            return View(cartDetailVM);
        }

        public IActionResult QuickAdd(Guid IdSP)
        {
            // Tim product muon them vao gio hang
            Product product = _iproductService.GetProductById(IdSP);

            // ktra xem gio hang da co SP nao hay chua. Co roi thi lay ra, Chua co thi khoi tao 1 list rong de ko bi loi
            List<CartDetail> cart = HttpContext.Session.GetJson<List<CartDetail>>("Cart") ?? new List<CartDetail>();

            // Ktra xem gio hang da co san pham muon them o dong dau hay chua
            CartDetail cartItem = cart.Where(p => p.IdSp == IdSP).FirstOrDefault();


            if (cartItem == null)
            {
                // Neu chua co thi tao 1 CartDetail moi tu ctor o Class Model Cartdetail
                cart.Add(new CartDetail(product));
            }
            else
            {
                // Neu co roi thi So luong san pham ++ 1
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);
            TempData["Success"] = "The product has been added";
            return RedirectToAction("Index");
        }

        public IActionResult Add(Guid IdSP, int Quantity)
        {
            // Tim product muon them vao gio hang
            Product product = _iproductService.GetProductById(IdSP);


            if(Quantity > product.AvailableQuantity)
            {
                
                return RedirectToAction("Details", "Product");
            }
            // ktra xem gio hang da co SP nao hay chua. Co roi thi lay ra, Chua co thi khoi tao 1 list rong de ko bi loi
            List<CartDetail>cart = HttpContext.Session.GetJson<List<CartDetail>>("Cart") ?? new List<CartDetail>();

            // Ktra xem gio hang da co san pham muon them o dong dau hay chua
            CartDetail cartItem = cart.Where(p => p.IdSp == IdSP).FirstOrDefault();
            
            
            if (cartItem == null)
            {  
                // Neu chua co thi tao 1 CartDetail moi tu ctor o Class Model Cartdetail
                cart.Add(new CartDetail(product, Quantity));
            }
            else
            {
                // Neu co roi thi So luong san pham ++ 1
                cartItem.Quantity += Quantity;
            }

            HttpContext.Session.SetJson("Cart", cart);
            TempData["Success"] = "The product has been added";
            return RedirectToAction("Index");
        }

        public IActionResult Decrease(Guid IdSP)
        {
            // Doc toan bo san pham tu Json sang 1 list<CartDetail>
            List<CartDetail> cart = HttpContext.Session.GetJson<List<CartDetail>>("Cart");

            // Kiem tra xem gio hang cart co san pham muon giam so luong hay khong
            CartDetail cartItem = cart.Where(p => p.IdSp == IdSP).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                // Neu co so luong > 1 => giam so luong di 1
                --cartItem.Quantity;
            }
            else
            {
                // Neu so luong = 1 va tiep tuc bam xoa => Xoa luon san pham ra khoi gio hang cart
                cart.RemoveAll(p => p.IdSp == IdSP);
            }

            if (cart.Count == 0)
            {
                // Neu gio hang rong => Xoa session
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }


        public IActionResult Remove(Guid IdSP)
        {
            List<CartDetail> cart = HttpContext.Session.GetJson<List<CartDetail>>("Cart");

            cart.RemoveAll(p => p.IdSp == IdSP);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }
    }
}
