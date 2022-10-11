using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Extensions
{
    public static class ModelBuilderExtensions
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductColor>().HasData(
                new ProductColor()
                {
                    Id = 1,
                    Color = ColorProduct.Yellow
                },
                new ProductColor()
                {
                    Id = 2,
                    Color = ColorProduct.Red
                },
                new ProductColor()
                {
                    Id = 3,
                    Color = ColorProduct.Green
                }
                );
            modelBuilder.Entity<ProductSize>().HasData(
                new ProductSize()
                {
                    Id = 1,
                    Size = 38
                },
                new ProductSize()
                {
                    Id = 2,
                    Size = 39
                },
                new ProductSize()
                {
                    Id = 3,
                    Size = 40
                }
                );
            //modelBuilder.Entity<ProductQuantity>().HasData(
            //    new ProductQuantity()
            //    {
            //        Id = 1,
            //        ProductId = 1,
            //        SizeId = 1,
            //        ColorId = 1,
            //    },
            //    new ProductQuantity()
            //    {
            //        Id = 2,
            //        ProductId = 1,
            //        SizeId = 2,
            //        ColorId = 2,
            //    },
            //    new ProductQuantity()
            //    {
            //        Id = 3,
            //        ProductId = 1,
            //        SizeId = 3,
            //        ColorId = 3,
            //    }
            //    );
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    SortOrder = 1,
                    Status = Shared.Enum.Status.Active,
                    Name = "Giày Adidas",
                    Url = "adidas-shoes"
                },
                new Category()
                {
                    Id = 2,
                    SortOrder = 1,
                    Status = Shared.Enum.Status.Active,
                    Name = "Giày Jordan",
                    Url = "jordan-shoes"
                },
                new Category()
                {
                    Id = 3,
                    SortOrder = 1,
                    Status = Shared.Enum.Status.Active,
                    Name = "Giày Nike",
                    Url = "nike-shoes"
                }
            );

            modelBuilder.Entity<ProductInCategory>().HasData(
                    new ProductInCategory() { CategoryId = 1, ProductId = 1 },
                    new ProductInCategory() { CategoryId = 1, ProductId = 2 },
                    new ProductInCategory() { CategoryId = 1, ProductId = 3 },
                    new ProductInCategory() { CategoryId = 1, ProductId = 4 },
                    new ProductInCategory() { CategoryId = 1, ProductId = 5 },
                    new ProductInCategory() { CategoryId = 1, ProductId = 6 },
                    new ProductInCategory() { CategoryId = 2, ProductId = 7 },
                    new ProductInCategory() { CategoryId = 2, ProductId = 8 },
                    new ProductInCategory() { CategoryId = 2, ProductId = 9 },
                    new ProductInCategory() { CategoryId = 2, ProductId = 10 },
                    new ProductInCategory() { CategoryId = 2, ProductId = 11 },
                    new ProductInCategory() { CategoryId = 2, ProductId = 12 },
                    new ProductInCategory() { CategoryId = 3, ProductId = 13 },
                    new ProductInCategory() { CategoryId = 3, ProductId = 14 },
                    new ProductInCategory() { CategoryId = 3, ProductId = 15 },
                    new ProductInCategory() { CategoryId = 3, ProductId = 16 },
                    new ProductInCategory() { CategoryId = 3, ProductId = 17 }
                    );
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Title = "ADIDAS ALPHABOOST “CORE BLACK”",
                    Description = "ADIDAS ALPHABOOST “CORE BLACK”",
                    Price = 2150000,
                    CategoryId = 1,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 2150000,
                     
                },
                new Product()
                {
                    Id = 2,
                    Title = "ADIDAS NMD R1 SERIAL PACK METAL GREY",
                    Description = "ADIDAS NMD R1 SERIAL PACK METAL GREY",
                    Price = 1650000,
                    CategoryId = 1,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 1650000,
                     
                },
                new Product()
                {
                    Id = 3,
                    Title = "ADIDAS SEAN WOTHERSPOON X SUPERSTAR ‘SUPER EARTH – BLACK’",
                    Description = "ADIDAS SEAN WOTHERSPOON X SUPERSTAR ‘SUPER EARTH – BLACK’",
                    Price = 3250000,
                    CategoryId = 1,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 3250000,
                     
                },
                new Product()
                {
                    Id = 4,
                    Title = "ADIDAS SUPERSTAR OG ‘VINTAGE WHITE’",
                    Description = "ADIDAS SUPERSTAR OG ‘VINTAGE WHITE’",
                    Price = 1650000,
                    CategoryId = 1,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 1650000,
                     
                },
                new Product()
                {
                    Id = 5,
                    Title = "ADIDAS NMD R1 TOKYO DRAGON",
                    Description = "ADIDAS NMD R1 TOKYO DRAGON",
                    Price = 1850000,
                    CategoryId = 1,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 1850000,
                     
                },
                new Product()
                {
                    Id = 6,
                    Title = "ADIDAS ULTRA BOOST 20 NASA CLOUD WHITE ",
                    Description = "ADIDAS ULTRA BOOST 20 NASA CLOUD WHITE ",
                    Price = 2550000,
                    CategoryId = 1,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 2550000,
                     
                },
                new Product()
                {
                    Id = 7,
                    Title = "AIR JORDAN 1 HIGH ‘BORDEAUX’",
                    Description = "AIR JORDAN 1 HIGH ‘BORDEAUX’",
                    Price = 6100000,
                    CategoryId = 2,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 6100000,
                     
                },
                new Product()
                {
                    Id = 8,
                    Title = "AIR JORDAN 1 HIGH OG “BUBBLE GUM”",
                    Description = "AIR JORDAN 1 HIGH OG “BUBBLE GUM”",
                    Price = 6450000,
                    CategoryId = 2,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 6450000,
                     
                },
                new Product()
                {
                    Id = 9,
                    Title = "AIR JORDAN 1 HIGH RETRO ‘HERITAGE’ GS ",
                    Description = "AIR JORDAN 1 HIGH RETRO ‘HERITAGE’ GS ",
                    Price = 4850000,
                    CategoryId = 2,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 4850000,
                     
                },
                new Product()
                {
                    Id = 10,
                    Title = "AIR JORDAN 1 HIGH ZOOM ‘CANYON RUST’ ",
                    Description = "AIR JORDAN 1 HIGH ZOOM ‘CANYON RUST’ ",
                    Price = 5550000,
                    CategoryId = 2,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 5550000,
                     
                },
                new Product()
                {
                    Id = 11,
                    Title = "AIR JORDAN 1 LOW GS TRIPLE WHITE ",
                    Description = "AIR JORDAN 1 LOW GS TRIPLE WHITE ",
                    Price = 3850000,
                    CategoryId = 2,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 3850000,
                     
                },
                new Product()
                {
                    Id = 12,
                    Title = "AIR JORDAN 1 LOW GS RUSH BLUE BRILL ",
                    Description = "AIR JORDAN 1 LOW GS RUSH BLUE BRILL ",
                    Price = 4350000,
                    CategoryId = 2,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 4350000,
                     
                },
                new Product()
                {
                    Id = 13,
                    Title = "CR7 X AIR MAX 97 GS ‘PORTUGAL PATCHWORK’ ",
                    Description = "CR7 X AIR MAX 97 GS ‘PORTUGAL PATCHWORK’ ",
                    Price = 4300000,
                    CategoryId = 3,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 4300000,
                     
                },
                new Product()
                {
                    Id = 14,
                    Title = "GIÀY NIKE DUNK LOW DISRUPT 2 ‘MALACHITE’ ",
                    Description = "GIÀY NIKE DUNK LOW DISRUPT 2 ‘MALACHITE’ ",
                    Price = 4850000,
                    CategoryId = 3,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 4850000,
                     
                },
                new Product()
                {
                    Id = 15,
                    Title = "NIKE AIR FORCE 1 LOW BY YOU CUSTOM – GUCCI ",
                    Description = "NIKE AIR FORCE 1 LOW BY YOU CUSTOM – GUCCI ",
                    Price = 3950000,
                    CategoryId = 3,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 3950000,
                     
                },
                new Product()
                {
                    Id = 16,
                    Title = "NIKE AIR FORCE 1 GS LOW WHITE PINK FOAM ",
                    Description = "NIKE AIR FORCE 1 GS LOW WHITE PINK FOAM ",
                    Price = 2950000,
                    CategoryId = 3,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 2950000,
                     
                },
                new Product()
                {
                    Id = 17,
                    Title = "NIKE AIR FORCE 1 GS WHITE UNIVERSITY RED ",
                    Description = "NIKE AIR FORCE 1 GS WHITE UNIVERSITY RED ",
                    Price = 2850000,
                    CategoryId = 3,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 2850000,
                     
                },
                new Product()
                {
                    Id = 18,
                    Title = "NIKE AIR FORCE 1 LOW ’07 ESSENTIAL WHITE METALLIC SILVER BLACK ",
                    Description = "NIKE AIR FORCE 1 LOW ’07 ESSENTIAL WHITE METALLIC SILVER BLACK ",
                    Price = 3200000,
                    CategoryId = 3,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 3200000,
                     
                }
            );

        }
    }
}
