using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Entities.OrderDto
{
    public class AssignShipperDto
    {
        public Guid ShipperId { get; set; }

        public string OrderId { get; set; }

    }
}
