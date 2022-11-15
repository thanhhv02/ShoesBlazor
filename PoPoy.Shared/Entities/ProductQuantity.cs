using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class ProductQuantity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; }
        public ProductSize Size { get; set; }
        public ProductColor Color { get; set; }
    }
}
