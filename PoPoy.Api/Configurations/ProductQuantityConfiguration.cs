using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoPoy.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Configurations
{
    public class ProductQuantityConfiguration : IEntityTypeConfiguration<ProductQuantity>
    {
        public void Configure(EntityTypeBuilder<ProductQuantity> builder)
        {
            builder.ToTable("ProductQuantities");
            builder.HasKey(x => x.Id);
        }
    }
}
