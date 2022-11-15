using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class ProductColor
    {
        public int Id { get; set; }
        public ColorProduct ColorName { get; set; }
        public List<ProductQuantity> ProductQuantities { get; set; }
    }
}
