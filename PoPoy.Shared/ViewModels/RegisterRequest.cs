﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels
{
    public class RegisterRequest
    {
        [Display(Name = "Tên")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [Display(Name = "Hòm thư")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [StringLength(10, ErrorMessage = "Số điện thoại phải 10 kí tự")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tài khoản")]
        [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "Tài phải từ 3 kí tự trở lên")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 kí tự trở lên")]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare(otherProperty: "Password", ErrorMessage = "Mật khẩu không trùng khớp")]
        public string ConfirmPassword { get; set; }
    }
}
