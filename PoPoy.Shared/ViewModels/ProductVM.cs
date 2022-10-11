﻿using PoPoy.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels
{
    public class ProductVM
    {
        public int Id { set; get; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Quantity { get; set; }
        public int Views { set; get; }
        public DateTime DateCreated { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public string ThumbnailImage { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public List<string> Sizes { get; set; } = new List<string>();
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductQuantity> ProductQuantities { get; set; }
    }
}
