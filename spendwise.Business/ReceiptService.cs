using System;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using spendwise.Business.Interfaces;
using spendwise.DataAccess.Entities;

namespace spendwise.Business
{
	public class ReceiptService : IReceiptService
	{
        private readonly string OCRURL = "https://netrom-ligaaclabs2024-dev.stage04.netromsoftware.ro/";


        public ReceiptService()
		{
		}

        public async Task<string> ScanReceipt(List<Category> categories, IFormFile image)
        {
            var imageOcr = await GetImageOcr(image);

            return imageOcr;
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

        #endregion
    }

}

