using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PoPoy.Shared.Dto;
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
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    SortOrder = 1,
                    Status = Shared.Enum.Status.Active,
                    Name = "Giày",
                    Url = "giay"
                }
            );

            modelBuilder.Entity<ProductInCategory>().HasData(
                    new ProductInCategory()
                    {
                        ProductId = 1,
                        CategoryId = 1
                    },
                    new ProductInCategory() { CategoryId = 1, ProductId = 2 },
                    new ProductInCategory() { CategoryId = 1, ProductId = 3 }
                    );
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Title = "Adidas",
                    Description = "Adidas",
                    Price = 100000000,
                    Quantity = 1000,
                    CategoryId = 1,
                },
                new Product()
                {
                    Id = 2,
                    Title = "Jordan",
                    Description = "Jordan",
                    Price = 100000000,
                    Quantity = 1000,
                    CategoryId = 1,
                },
                new Product()
                {
                    Id = 3,
                    Title = "Nike",
                    Description = "Nike",
                    Price = 100000000,
                    Quantity = 1000,
                    CategoryId = 1,
                }
            );
        }
    }
}
