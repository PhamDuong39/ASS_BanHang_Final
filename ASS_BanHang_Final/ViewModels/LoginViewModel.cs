using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace ASS_BanHang_Final.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Điện thoại / Email")]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }
}
