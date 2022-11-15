using PoPoy.Shared.Enum;
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
        public string OrderId { set; get; }
        public string OrderIdFromOrder { get; set; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public string SizeName { get; set; }
        public ColorProduct ColorName { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal Price { set; get; }
        public double TotalPrice { set; get; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
