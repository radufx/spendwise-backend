using System;
using spendwise.DataAccess.Dtos;
using spendwise.DataAccess.Entities;

namespace spendwise.Business.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<GetProductDto>> GetProductsAsync();

		Task<GetProductDto?> FindProductByIdAsync(int id);

		Task<Product> CreateProductAsync(CreateProductDto product);
	}
}

