using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels.ProductColor
{
    internal class ProductColorFillter
    {
        public int? Id { get; set; }

        public string ColorName { get; set; }

        public bool Checked { get; set; }
    }
}
