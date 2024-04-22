using System;
namespace spendwise.DataAccess.Dtos
{
	public class CategorizedProductsDto
	{
		public string Name { get; set; }
		public int Id { get; set; }
		public List<ScannedProductDto> Products { get; set; }

		public CategorizedProductsDto()
		{
		}
	}
}

