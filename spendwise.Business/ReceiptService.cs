using System;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using spendwise.Business.Exceptions;
using spendwise.Business.Interfaces;
using spendwise.DataAccess.Dtos;
using spendwise.DataAccess.Entities;
using spendwise.DataAccess.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace spendwise.Business
{
	public class ReceiptService : IReceiptService
	{
        private readonly string OCRURL = "https://netrom-ligaaclabs2024-dev.stage04.netromsoftware.ro/";
        private const string CHAT_GPT_URL = "https://netrom-ligaaclabs2024chatgpt-dev.stage04.netromsoftware.ro/";
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Category> _categoryRepository;

        public ReceiptService(IRepository<Product> productRepository, IRepository<Cart> cartRepository, IRepository<Category> categoryRepository)
		{
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _categoryRepository = categoryRepository;
		}

        public async Task<List<CategorizedProductsDto>> ScanReceipt(List<Category> categories, IFormFile image)
        {
            var imageOcr = await GetImageOcr(image);
            var categorizedProducts = await GetCategorizedProducts(categories, imageOcr);

            var deserializedCategorizedProducts = JsonConvert.DeserializeObject<string>(categorizedProducts);
            var categoriesProducts = JsonConvert.DeserializeObject < Dictionary<string, List<ScannedProductDto>>>(deserializedCategorizedProducts);

            var categorizedProductsDtoList = new List<CategorizedProductsDto>();
            foreach (var category in categoriesProducts)
            {
                var products = category.Value.DistinctBy(p => new { p.Name, p.Price }).ToList();

                foreach(var product in products)
                {
                    product.Quantity = category.Value.Where(p => p.Name == product.Name && p.Price == product.Price).Sum(p => p.Quantity);
                }

                categorizedProductsDtoList.Add(new CategorizedProductsDto
                {
                    Name = category.Key,
                    Id = categories.First(c => c.Name == category.Key).Id,
                    Products = products
                }) ;
            }

            return categorizedProductsDtoList;
        }

        public async Task<Cart> SaveCart(CreateCartDto createCart)
        {
            var existingProducts = await _productRepository.GetAllAsync();

            var cart = new Cart
            {
                Date = createCart.date
            };

            var cartProducts = new List<CartProduct>();

            foreach(var categoryProducts in createCart.CategoryProducts)
            {
                var category = await _categoryRepository.FindByIdAsync(categoryProducts.Id);

                if (category == null) throw new NotFoundException($"Entity of type {typeof(Category)} not found");

                cartProducts.AddRange(await AddProducts(category, categoryProducts.Products, existingProducts.ToList()));
            }

            cart.Items = cartProducts;

            await _cartRepository.PostAsync(cart);

            return cart;
        }

        #region Private methods

        private async Task<string> GetImageOcr(IFormFile image)
        {
            var imageOcr = string.Empty;

            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            var bytes = ms.ToArray();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(OCRURL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(bytes), "image", "image.jpg");

            var response = client.PostAsync("", content).Result;
            if (response.IsSuccessStatusCode)
            {
                imageOcr = await response.Content.ReadAsStringAsync();
            }

            await ms.DisposeAsync();
            client.Dispose();

            return imageOcr;
        }

        private async Task<string> GetCategorizedProducts(List<Category> categories, string imageOcr)
        {
            var categorizedProducts = string.Empty;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(CHAT_GPT_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(imageOcr), "ocr");
            content.Add(new StringContent(JsonConvert.SerializeObject(categories.Select(c => c.Name))), "categories");

            var response = client.PostAsync("", content).Result;
            if (response.IsSuccessStatusCode)
            {
                categorizedProducts = await response.Content.ReadAsStringAsync();
            }

            client.Dispose();

            return categorizedProducts;
        }

        private async Task<List<CartProduct>> AddProducts(Category category, List<ScannedProductDto> scannedProducts, List<Product> existingProducts)
        {
            var cartProducts = new List<CartProduct>();

            foreach(var scannedProduct in scannedProducts)
            {
                var existingProduct = existingProducts.Find(p => p.Name == scannedProduct.Name);

                // product wasn't added yet
                if (existingProduct == null)
                {
                    var product = new Product
                    {
                        Name = scannedProduct.Name,
                        Categories = new List<Category> { category }
                    };

                    await _productRepository.PostAsync(product);

                    var cartProduct = new CartProduct
                    {
                        Product = product,
                        Quantity = scannedProduct.Quantity,
                        Price = scannedProduct.Price
                    };

                    cartProducts.Add(cartProduct);
                } else
                {
                    if (!existingProduct.Categories.Contains(category))
                    {
                        existingProduct.Categories.Add(category);

                        await _productRepository.UpdateAsync(existingProduct);
                    }

                    var cartProduct = new CartProduct
                    {
                        Product = existingProduct,
                        Quantity = scannedProduct.Quantity,
                        Price = scannedProduct.Price
                    };

                    cartProducts.Add(cartProduct);
                }

            }

            return cartProducts;
        }

        #endregion
    }

}

