using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace spendwise.DataAccess.Dtos
{
	public class ScannedProductDto
	{
		[JsonProperty("nume produs")]
		public string Name { get; set; }
		[JsonProperty("cantitate")]
		public int Quantity { get; set; }
		[JsonProperty("pret")]
		public float Price { get; set; }
	}
}

