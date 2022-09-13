using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class Cart
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal Price { set; get; }
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
        public decimal CalcAmount()
        {
            return Math.Round(Quantity * Product.Price, MidpointRounding.AwayFromZero);
        }

        public CartStorage ToCartStorage()
        {
            return new CartStorage
            {
                ProductId = Product.Id,
                Quantity = Quantity
            };
        }
    }
}
