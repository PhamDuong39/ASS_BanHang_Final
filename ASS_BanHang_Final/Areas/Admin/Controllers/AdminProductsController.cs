using ASS_BanHang_Final.Extensions;
using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASS_BanHang_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly IProductService _iproductService;


        public AdminProductsController()
        {
            _iproductService = new ProductService();
        }

        public IActionResult Index()
        {
            List<Product> lstprd = new List<Product>();
            lstprd = _iproductService.GetAllProduct();
        
            return View(lstprd);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var obj = _iproductService.GetAllProduct();
            if (product.AvailableQuantity < 0 || product.Price <= 0)
            {
                return RedirectToAction("Index");
            }
            

            if (_iproductService.CreateProduct(product))
            {
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Error");
        }

        [HttpGet]
        public IActionResult Details(Guid Id)
        {
            var obj = _iproductService.GetProductById(Id);
            return View(obj);
        }


        public IActionResult Delete(Guid Id)
        {
            var deleteProduct = _iproductService.GetProductById(Id);
            HttpContext.Session.SetJson("Rollback", deleteProduct);
            var obj = _iproductService.DeleteProduct(Id);
            return RedirectToAction("Index");
        }

        public IActionResult RollBackList()
        {
            Product lstRollBack =  HttpContext.Session.GetJson<Product>("Rollback"); 
            return View(lstRollBack);
        }

        public IActionResult RollBackToDB()
        {
            var oldProduct = HttpContext.Session.GetJson<Product>("Rollback");
            Product product = new Product();
            product.Id = oldProduct.Id;
            product.Price = oldProduct.Price;
            product.Supplier = oldProduct.Supplier;
            product.Name = oldProduct.Name;
            product.Description = oldProduct.Description;
            product.AvailableQuantity = oldProduct.AvailableQuantity;
            product.Status = oldProduct.Status;
            
            _iproductService.CreateProduct(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid Id)
        {
            var obj = _iproductService.GetProductById(Id);
            return View(obj);
        }

        
        public IActionResult Edit(Product product)
        {
            var obj = _iproductService.GetProductById(product.Id);
            if ( obj.AvailableQuantity < 0)
            {
                return RedirectToAction("Index");
            }
            if (_iproductService.UpdateProduct(product))
            {
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Error");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
