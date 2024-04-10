using System;
using spendwise.DataAccess.Dtos;
using spendwise.DataAccess.Entities;

namespace spendwise.Business.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
	}
}

