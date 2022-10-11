using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels
{
    public class ProductUpdateRequest
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Quantity { set; get; }
    }
}
