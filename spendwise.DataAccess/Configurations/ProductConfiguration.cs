using System;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using spendwise.DataAccess.Entities;

namespace spendwise.DataAccess.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product").HasKey(c => c.Id);

            builder.Property(c => c.Name).HasMaxLength(255);

            builder.HasMany(p => p.Categories).WithMany(c => c.Products).
                UsingEntity<Dictionary<string, object>>("ProductCategory",
                    e => e.HasOne<Category>().WithMany().HasForeignKey("CategoryId").HasConstraintName("FK_ProductCategory_Category"),
                    e => e.HasOne<Product>().WithMany().HasForeignKey("ProductId").HasConstraintName("FK_ProductCategory_Product"));

           
        }
    }
}

