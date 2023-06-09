using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASS_BanHang_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRolesController : Controller
    {
        private readonly IRoleService _iroleService;
        public AdminRolesController()
        {
            _iroleService = new RoleService();
        }

        public IActionResult Index()
        {
            var lst = _iroleService.GetAllRoles();
            return View(lst);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Role role)
        {
            _iroleService.CreateRole(role);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var obj = _iroleService.GetRoleById(id);
            return View(obj);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var obj = _iroleService.GetRoleById(id);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Role role)
        {
            if (_iroleService.CreateRole(role))
            {
                return RedirectToAction("Index");
            }
            else return BadRequest();
        }

        public IActionResult Delete(Guid id)
        {
            _iroleService.DeleteRole(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
