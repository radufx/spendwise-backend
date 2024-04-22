using System;
using Microsoft.AspNetCore.Http;
using spendwise.DataAccess.Dtos;
using spendwise.DataAccess.Entities;

namespace spendwise.Business.Interfaces
{
	public interface IReceiptService
	{
		public Task<List<CategorizedProductsDto>> ScanReceipt(List<Category> categories, IFormFile image);

		public Task<Cart> SaveCart(CreateCartDto createCart);
	}
}

