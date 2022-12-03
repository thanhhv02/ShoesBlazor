using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        [Column(TypeName = "decimal(18,0)")]
        //public decimal Price { get; set; }
        //[Column(TypeName = "decimal(18,0)")]
        //public decimal OriginalPrice { get; set; }
        public decimal ReviewAverage { get; set; } = 0;
        public int CheckoutCount { get; set; } 
        public int CategoryId { get; set; }
        public int Views { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Cart> Carts { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<ProductQuantity> ProductQuantities { get; set; }
        
    }
}
