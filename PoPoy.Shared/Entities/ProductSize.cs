﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class ProductSize
    {
        public int Id { get; set; }
        public string SizeName { get; set; }
        public List<ProductQuantity> ProductQuantities { get; set; }
    }
}
