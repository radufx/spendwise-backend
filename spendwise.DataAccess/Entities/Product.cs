using System;


namespace spendwise.DataAccess.Entities
{
	public class Product
	{
		public string Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        public ICollection<Cart> Carts { get; set; } = new HashSet<Cart>();
        public ICollection<CartProduct> ProductCarts { get; set; } = new HashSet<CartProduct>();
    }
}

