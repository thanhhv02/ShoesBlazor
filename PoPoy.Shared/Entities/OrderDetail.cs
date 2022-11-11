using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class OrderDetail
    {
        public User User { get; set; }
        public OrderDetails OrderDetails { get; set; }
    }
}
