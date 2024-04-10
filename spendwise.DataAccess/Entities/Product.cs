using System;
using System.Text.Json.Serialization;

namespace spendwise.DataAccess.Entities
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        public ICollection<Cart> Carts { get; set; } = new HashSet<Cart>();
        public ICollection<CartProduct> ProductCarts { get; set; } = new HashSet<CartProduct>();
    }
}

