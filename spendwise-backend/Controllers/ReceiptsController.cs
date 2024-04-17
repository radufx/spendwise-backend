using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using spendwise.Business.Interfaces;
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

			var imageOcr = await _receiptService.ScanReceipt(categoriesList, image);

			return Ok(imageOcr);
		}
	}
}

