
using System;
namespace spendwise.DataAccess.Dtos
{
	public class GetProductDto : ProductDto
	{
		public ICollection<string> Categories { get; set; } = new HashSet<string>(); 
	}
}

