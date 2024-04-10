using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using spendwise.DataAccess.Entities;

namespace spendwise.DataAccess.Configurations
{
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.ToTable("CartProducts")
                .HasKey(cp => new
                {
                    cp.CartId,
                    cp.ProductId
                });

            builder.Property(cp => cp.Quantity)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(cp => cp.Price)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne(cp => cp.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(cp => cp.CartId);

            builder.HasOne(cp => cp.Product)
                .WithMany(p => p.ProductCarts)
                .HasForeignKey(cp => cp.ProductId);
        }
    }
}

