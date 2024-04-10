using System;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using spendwise.DataAccess.Entities;

namespace spendwise.DataAccess.Configurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Category").HasKey(c => c.Id);

			builder.Property(c => c.Name).HasMaxLength(255);

			builder.HasData(new Category { Id = 1, Name = "others" });
		}
	}
}

