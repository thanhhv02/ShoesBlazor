using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels
{
    public class ProductColorDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Tên màu là bắt buộc")]

        public string ColorName { get; set; }
    }
}
