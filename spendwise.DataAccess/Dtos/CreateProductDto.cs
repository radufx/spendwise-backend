using System;
namespace spendwise.DataAccess.Dtos
{
	public class CreateProductDto
	{
		public string Name { get; set; }
		public ICollection<int> Categories { get; set; } = new HashSet<int>();
	}
}

