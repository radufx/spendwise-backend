using System;
using Microsoft.AspNetCore.Mvc;
using spendwise.Business;
using spendwise.Business.Interfaces;
using spendwise.DataAccess.Dtos;

namespace spendwise_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProductsAsync();

            return Ok(products);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto product)
        {
            var createdProduct = await _productService.CreateProductAsync(product);

            return Ok(createdProduct);
        }

        [HttpGet("{id}/GetProduct")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var product = await _productService.FindProductByIdAsync(id);

            if (product == null)
            {
                return NotFound(id);
            }

            return Ok(product);
        }
    }
}

