using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Entities.OrderDto
{
    public class OrderShipperSearchDto
    {

        public Guid ShipperId { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Processing;
    }
}
