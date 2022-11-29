using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels
{
    public class CategoryCreateRequest
    {
        [Display(Name = "Tên loại hàng")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string Name { get; set; }
        public Status Status { get; set; }
        public int SortOrder { get; set; }
        public string Url { get; set; }
    }
}
