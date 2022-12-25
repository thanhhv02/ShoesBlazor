using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels
{
    public class UserVM
    {
        public Guid Id { get; set; }

        [Display(Name = "Tên")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        public string LastName { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu mã hóa")]
        public string Password { get; set; } = string.Empty;


        [Compare("Password", ErrorMessage = "Mật khẩu nhập lại không đúng" )]
        public string PasswordConfirm { get; set; } = string.Empty;
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Ngày sinh")]

        public DateTime Dob { get; set; }

        public IList<string> Roles { get; set; }
    }
}
