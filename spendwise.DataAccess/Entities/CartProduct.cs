using System;
namespace spendwise.DataAccess.Entities
{
	public class CartProduct
	{
		public Product Product { get; set; }
		public int ProductId { get; set; }
		public Cart Cart { get; set; }
		public int CartId { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
	}
}

