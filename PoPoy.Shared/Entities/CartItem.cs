using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class CartItem
    {
        public int Quantity { set; get; }
        public Product Product { set; get; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
