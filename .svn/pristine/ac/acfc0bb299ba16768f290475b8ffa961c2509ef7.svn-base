using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Phải nhập tài khoản")]
        public string UserName { get; set; }

        [Required]
        [StringLength(int.MaxValue ,MinimumLength = 6 , ErrorMessage = "Mật khẩu phải từ 6 kí tự trở lên")]
        public string Password { get; set; }
    }
}
