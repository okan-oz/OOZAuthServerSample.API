using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOZAuthServereSample.Core.Model;

namespace OOZAuthServerSample.Data.Configuration
{
    public class ProductConfiguration:IEntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.Property(x=>x.UserId).IsRequired();

        }
    }
}
