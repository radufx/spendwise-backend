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

        public async Task<Category> CreateCategoryAsync(CreateCategoryDto category)
        {
            var newCategory = new Category
            {
                Id = 0,
                Name = category.Name,
            };

            return await _categoriesRepository.PostAsync(newCategory);
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

        public async Task<CategoryDto?> FindCategoryByIdAsync(int id)
        {
            var category = await _categoriesRepository.FindByIdAsync(id);

            if(category == null)
            {
                return null;
            }

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
            };

            return categoryDto;
        }

        public async Task<Category?> UpdateCategoryAsync(UpdateCategoryDto category)
        {
            var existingCategory = await _categoriesRepository.FindByIdAsync(category.Id);
            if (existingCategory == null)
            {
                return null;
            }

            // TODO: create updatecategory DTO only with name
            //existingCategory.Name = category.Name;

            var updatedCategory = new Category
            {
                Id = category.Id,
                Name = category.Name
            };

            return await _categoriesRepository.UpdateAsync(existingCategory);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoriesRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CategoryTotalPriceDto>> GetCategoriesTotal(DateTime? dateFrom,DateTime? dateTo)
        {
            var categories = await _categoriesRepository.GetAllAsync();

            var categoriesTotal = new List<CategoryTotalPriceDto>();

            foreach (var category in categories)
            {
                float totalCategory = 0;

                foreach (var product in category.Products)
                {
                    totalCategory = product.ProductCarts.Sum(pc => {
                        if (dateFrom != null && dateTo != null)
                        {
                            if (pc.Cart.Date >= dateFrom && pc.Cart.Date <= dateTo) return pc.Price * pc.Quantity;
                            else return 0;
                        }
                        else return pc.Price * pc.Quantity;
                     });
                }


                categoriesTotal.Add(new CategoryTotalPriceDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Total = totalCategory
                });
            }

            return categoriesTotal;
        }
    }
}

