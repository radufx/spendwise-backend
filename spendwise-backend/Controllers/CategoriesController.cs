using System;
using Microsoft.AspNetCore.Mvc;
using spendwise.Business.Interfaces;

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
	}
}

