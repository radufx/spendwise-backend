using System;
using spendwise.Business.Interfaces;
using spendwise.DataAccess.Dtos;
using spendwise.DataAccess.Entities;
using spendwise.DataAccess.Repositories;

namespace spendwise.Business
{
	public class CategoryService : ICategoryService
	{
        private readonly IRepository<Category> _categoriesRepository;

		public CategoryService(IRepository<Category> categoriesRepository)
		{
            _categoriesRepository = categoriesRepository;
		}

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoriesRepository.GetAllAsync();

            var categoriesDtos = new List<CategoryDto>();
            categoriesDtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
            }).ToList();

            return categoriesDtos;
        }
    }
}

