using System;
namespace spendwise.DataAccess.Entities
{
	public class CartProduct
	{
		public Product Product { get; set; }
		public string ProductId { get; set; }
		public Cart Cart { get; set; }
		public string CartId { get; set; }
		public int Quantity { get; set; }
		public float Price { get; set; }
	}
}

