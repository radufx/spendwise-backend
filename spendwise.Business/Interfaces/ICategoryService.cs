using System;
using spendwise.DataAccess.Dtos;
using spendwise.DataAccess.Entities;

namespace spendwise.Business.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

		Task<Category> CreateCategoryAsync(CreateCategoryDto category);

		Task<CategoryDto?> FindCategoryByIdAsync(int id);

		Task<Category?> UpdateCategoryAsync(UpdateCategoryDto category);

		Task DeleteCategoryAsync(int id);
	}
}

