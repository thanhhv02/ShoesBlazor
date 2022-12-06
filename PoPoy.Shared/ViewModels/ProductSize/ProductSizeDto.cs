using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels
{
    public class ProductSizeDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Kích thước là bắt buộc")]
        public string SizeName { get; set; }
    }
}
