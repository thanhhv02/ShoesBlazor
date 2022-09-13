using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class Order
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,0)")]
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }
        public User User { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
