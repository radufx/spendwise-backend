using System;
namespace spendwise.DataAccess.Entities
{
	public class Category
	{

		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;

		public ICollection<Product> Products { get; set; } = new HashSet<Product>();
		
	}
}

