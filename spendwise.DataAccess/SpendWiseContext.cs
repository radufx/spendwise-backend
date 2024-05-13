using System;
using Microsoft.EntityFrameworkCore;
using spendwise.DataAccess.Configurations;
using spendwise.DataAccess.Entities;

namespace spendwise.DataAccess
{
	public class SpendWiseContext : DbContext
	{
		public SpendWiseContext(DbContextOptions<SpendWiseContext> options): base(options)
		{

		}

		public DbSet<Category> Categories { set; get; }
		public DbSet<Product> Products { set; get; }
		public DbSet<CartProduct> CartProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
			new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
			new CartConfiguration().Configure(modelBuilder.Entity<Cart>());
			new CartProductConfiguration().Configure(modelBuilder.Entity<CartProduct>());

		}
    }
}

