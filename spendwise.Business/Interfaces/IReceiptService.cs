using System;
using Microsoft.AspNetCore.Http;
using spendwise.DataAccess.Entities;

namespace spendwise.Business.Interfaces
{
	public interface IReceiptService
	{
		public Task<string> ScanReceipt(List<Category> categories, IFormFile image);
	}
}

