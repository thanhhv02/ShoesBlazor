using PoPoy.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Paging
{
    public class ProductParameters: QueryStringParameters
    {
        public string OrderBy { get; set; } = "name";
        public string searchText { get; set; } = null;
        public string[] ColorId { get; set; } = null;
        public string[] SizeId { get; set; } = null;
    }
}
