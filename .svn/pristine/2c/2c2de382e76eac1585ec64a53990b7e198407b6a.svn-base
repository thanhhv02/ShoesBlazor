using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels
{
    public class ChangePasswordRequest
    {
        public string UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu cũ")]
        public string CurrentPassword { get; set; }
        //[Required, Range(6, int.MaxValue)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Hai mật khẩu không giống nhau")]
        public string ConfirmPassword { get; set; }
    }
}
