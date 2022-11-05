using PoPoy.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Common
{
    public class ProductFilterParameters
    {
        public Category CategoryName { get; set; } = null;
        public bool Lastest { get; set; }
        public bool MostView { get; set; }
        public bool MostRating { get; set; }
        public bool AlphaBet { get; set; }

    }
}
