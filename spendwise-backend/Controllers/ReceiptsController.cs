using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using spendwise.Business.Exceptions;
using spendwise.Business.Interfaces;
using spendwise.DataAccess.Dtos;
using spendwise.DataAccess.Entities;

namespace spendwise_backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReceiptsController : ControllerBase
	{
		private readonly IReceiptService _receiptService;

		public ReceiptsController(IReceiptService receiptService)
		{
			_receiptService = receiptService;
		}


		[HttpGet("ScanReceipt")]
		public async Task<ActionResult<string>> ScanReceipt([FromForm] string categories, [FromForm] IFormFile image)
		{
			List<Category> categoriesList = JsonConvert.DeserializeObject<List<Category>>(categories) ?? new List<Category>();

			if (categoriesList.Count() == 0 || image.Length <= 0)
			{
				return BadRequest();
			}

			var categorizedProductsDto = await _receiptService.ScanReceipt(categoriesList, image);

			return Ok(categorizedProductsDto);
		}

		[HttpPost("SaveCart")]
		public async Task<ActionResult<string>> SaveCart([FromBody] CreateCartDto cart)
		{
			if (cart.CategoryProducts.Count() == 0)
			{
				return BadRequest();
			}

			try
			{
			var createdCart = await _receiptService.SaveCart(cart);

			}
			catch(NotFoundException e)
			{
				return BadRequest(e.Message);
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
				return BadRequest();
			}

			return Ok();
		}
	}
}

