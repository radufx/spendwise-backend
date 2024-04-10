using System;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using spendwise.DataAccess.Entities;

namespace spendwise.DataAccess.Configurations
{
	public class CartConfiguration: IEntityTypeConfiguration<Cart>
	{
		public void Configure(EntityTypeBuilder<Cart> builder)
		{
			builder.ToTable("Cart").HasKey(c => c.Id);
		}
	}
}

