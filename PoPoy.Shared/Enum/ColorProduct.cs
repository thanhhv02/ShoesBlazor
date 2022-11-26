using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PoPoy.Shared.Enum
{
    public enum ColorProduct
    {
        [Display(Name = "Không màu")]
        NoColor,
        [Display(Name = "Đỏ")]
        Red,
        [Display(Name = "Vàng")]
        Yellow,
        [Display(Name = "Đen")]
        Black,
        [Display(Name = "Xanh lá")]
        Green,
        [Display(Name = "Xanh dương")]
        Blue,
        [Display(Name = "Xám")]
        Grey,
        [Display(Name = "Trắng")]
        White,
    }
}
