using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels.Report
{
    public class ReportOrderNew
    {
        public string OrderId { get; set; }

        public string FullName { get; set; }

        public string ProductName { get; set; }

        public int ProductId { get; set; }
        public decimal Price { get; set; }

        public OrderStatus Status { get; set; }
    }
}
