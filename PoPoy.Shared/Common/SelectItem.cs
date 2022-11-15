using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Common
{
    public class SelectItem
    {
        public string Id { get; set; }
        public string ColorId { get; set; }
        public string ColorName { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public bool Selected { get; set; }
        public decimal Price { get; set; }
    }
}
