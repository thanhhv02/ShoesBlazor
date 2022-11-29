using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels
{
    public class ProductCreateRequest
    {
        //public decimal Price { set; get; }
        //public decimal OriginalPrice { set; get; }
        [MaxLength(50)]
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string Title { set; get; }
        public int Quantity { get; set; }
        public string Description { set; get; }
    }
}
