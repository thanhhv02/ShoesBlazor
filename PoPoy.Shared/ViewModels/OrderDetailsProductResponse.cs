using PoPoy.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels
{
    public class OrderDetailsProductResponse
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ProductType { get; set; }
        public int ProductSize { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
