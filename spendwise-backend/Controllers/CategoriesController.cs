using System;
using Microsoft.AspNetCore.Mvc;
using spendwise.Business.Interfaces;
using spendwise.DataAccess.Entities;
using spendwise.DataAccess.Dtos;

namespace spendwise_backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet("GetCategories")]
		public async Task<IActionResult> GetCategories()
		{
			var categories = await _categoryService.GetCategoriesAsync();

			return Ok(categories);
		}

		[HttpPost("AddCategory")]
		public async Task<IActionResult> AddCategory([FromBody]CreateCategoryDto category)
		{
			var createdCategory = await _categoryService.CreateCategoryAsync(category);

			return Ok(createdCategory);
		}

		[HttpGet("{id}/GetCategory")]
		public async Task<IActionResult> GetCategory([FromRoute] int id)
		{
			var category = await _categoryService.FindCategoryByIdAsync(id);

			if (category == null)
			{
				return NotFound(id);
			}

			return Ok(category);
		}

		[HttpPut("{id}/UpdateCategory")]
		public async Task <IActionResult> UpdateCategory([FromRoute] int id, [FromBody]UpdateCategoryDto updatedCategory)
		{
            var category = await _categoryService.UpdateCategoryAsync(updatedCategory);

            if (category == null)
            {
                return NotFound(id);
            }

            return Ok(category);
        }

		[HttpDelete("{id}/DeleteCategory")]
		public async Task<IActionResult> DeleteCategory([FromRoute] int id)
		{
			// TODO: maybe add error handling for unexisting category
			await _categoryService.DeleteCategoryAsync(id);

			return Ok();
		}

		[HttpGet("GetCategoriesTotal")]
		public async Task<IActionResult> GetCategoriesTotal([FromQuery] DateTime? dateFrom = null, [FromQuery] DateTime? dateTo = null)
		{
			var categoriesTotal = await _categoryService.GetCategoriesTotal(dateFrom, dateTo);

			return Ok(categoriesTotal);
		}	
    }
}

