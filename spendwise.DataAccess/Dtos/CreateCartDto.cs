using System;
namespace spendwise.DataAccess.Dtos
{
	public class CreateCartDto
	{
		public DateTime date { get; set; }
		public List<CategorizedProductsDto> CategoryProducts { get; set; }

	}
}

