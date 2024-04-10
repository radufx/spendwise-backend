using System;
namespace spendwise.DataAccess.Entities
{
	public class Cart
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public ICollection<Product> Products { get; set; } = new HashSet<Product>();
		public ICollection<CartProduct> Items { get; set; } = new HashSet<CartProduct>();
	}
}

