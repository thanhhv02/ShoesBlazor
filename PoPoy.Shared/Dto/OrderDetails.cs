using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class OrderDetails
    {
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal Price { set; get; }
        public string Name { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
