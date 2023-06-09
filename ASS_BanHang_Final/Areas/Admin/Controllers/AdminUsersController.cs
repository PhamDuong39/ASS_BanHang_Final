using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace ASS_BanHang_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserService _iuserService;
        private readonly IRoleService _iroleService;

        public AdminUsersController()
        {
            _iuserService = new UserService();
            _iroleService = new RoleService();
        }

        public IActionResult Index()
        {
            List<User> lst = new List<User>();
            lst = _iuserService.GetAllUser();

            List<Role> lstRole = new List<Role>();
            lstRole = _iroleService.GetAllRoles();
            ViewData["RoleList"] = new SelectList(lstRole, "Id", "RoleName");
           
            List<SelectListItem> lstTrangThai = new List<SelectListItem>();
            lstTrangThai.Add(new SelectListItem() { Text = "Active", Value = "1"});
            lstTrangThai.Add(new SelectListItem() { Text = "Block", Value = "0" });
            ViewData["StatusList"] = lstTrangThai;

            return View(lst);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var obj = _iuserService.GetUserById(id);
            return View(obj);
        }

        public IActionResult Delete(Guid id)
        {
            _iuserService.DeleteUser(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var obj = _iuserService.GetUserById(id);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(User obj)
        {
            if (_iuserService.UpdateUser(obj))
            {
                return RedirectToAction("Index");
            }
            else return BadRequest();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User obj)
        {
            _iuserService.CreateUser(obj);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
