using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.Services;
using ASS_BanHang_Final.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.RegularExpressions;

namespace ASS_BanHang_Final.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _iuserService;
        private readonly IRoleService _iroleService;
     
    

        public AccountController(IUserService userService, IRoleService roleService)
        {
            _iuserService = userService;
            _iroleService = roleService;
          
        }


        public IActionResult LoginDiThi()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginDiThi(string Username, string Password)
        {

            var isHaveAccount = _iuserService.Login(Username, Password);
            if (isHaveAccount)
            {

                var roleId = _iuserService.GetRoleIdByUsername(Username);
                var roleName = _iroleService.GetRoleById(roleId).RoleName;
                if (roleName == "Admin")
                {
                    HttpContext.Session.SetString("admin", Username);
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    HttpContext.Session.SetString("username", Username);
                    var userID = _iuserService.GetUserIdByUsername(Username);
                    HttpContext.Session.SetString("UserID", userID.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            else return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Username, string Password)
        {
            //string pattern = "^[a-zA-Z0-9]{6,}$";
            //if(Regex.IsMatch(Username, pattern) == false)
            //{
            //    return RedirectToAction("Login");
            //}
            // Check xem c
            var isHaveAccount = _iuserService.Login(Username, Password);
            if (isHaveAccount)
            {
               
                var roleId = _iuserService.GetRoleIdByUsername(Username);
                var roleName = _iroleService.GetRoleById(roleId).RoleName;
                if (roleName == "Admin")
                {
                    HttpContext.Session.SetString("admin", Username);
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    HttpContext.Session.SetString("username", Username);
                    var userID = _iuserService.GetUserIdByUsername(Username);
                    HttpContext.Session.SetString("UserID", userID.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            else return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKyTaiKhoan(RegisterViewModel registerVM)
        {
            if(_iuserService.GetAllUser().Any(p => p.Username == registerVM.Username))
            {
                ModelState.AddModelError("Username", "This username is already taken");
                return RedirectToAction("");
            }

            Guid idUserRole = _iroleService.GetAllRoles().FirstOrDefault(p => p.RoleName == "User").Id;

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = registerVM.Username,
                Password = registerVM.Password,
                Status = 1,
                RoleId = idUserRole
            };

            _iuserService.CreateUser(user);

            return RedirectToAction("Login", "Account");
        }

        public IActionResult LogOutUser()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOutAdmin()
        {
            HttpContext.Session.Remove("admin");

            return RedirectToAction("Index", "Home");
        }
    }
}
