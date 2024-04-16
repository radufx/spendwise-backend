using System;
using spendwise.Business.Interfaces;
using spendwise.DataAccess.Dtos;
using spendwise.DataAccess.Entities;
using spendwise.DataAccess.Repositories;

namespace spendwise.Business
{
	public class ProductService : IProductService
	{
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Category> _categoriesRepository;


        public ProductService(IRepository<Product> productsRepository, IRepository<Category> categoriesRepository)
		{
            _productsRepository = productsRepository;
            _categoriesRepository = categoriesRepository;
		}

        public async Task<Product> CreateProductAsync(CreateProductDto product)
        {
            var categories = (await _categoriesRepository.GetAllAsync())
                        .Where(c => product.Categories.Contains(c.Id)).ToList();

            var newProduct = new Product
            {
                Id = 0,
                Name = product.Name,
                Categories = categories
            };

            return await _productsRepository.PostAsync(newProduct);
        }

        public async Task<GetProductDto?> FindProductByIdAsync(int id)
        {
            var product = await _productsRepository.FindByIdAsync(id);

            if (product == null) return null;

            var productDto = new GetProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Categories = product.Categories.Select(c => c.Name).ToList()
            };

            return productDto;
        }

        public async Task<IEnumerable<GetProductDto>> GetProductsAsync()
        {
            var products = await _productsRepository.GetAllAsync();

            var productDtos = new List<GetProductDto>();
            productDtos = products.Select(p => new GetProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Categories = p.Categories.Select(c => c.Name).ToList()
            }).ToList();

            return productDtos;
        }
    }
}

