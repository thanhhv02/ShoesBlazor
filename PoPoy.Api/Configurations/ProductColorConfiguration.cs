using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoPoy.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Configurations
{
    public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder.ToTable("ProductColors");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.ColorName)
        .HasConversion<string>();
        }
    }
}
