using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CodixioCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace CodixioCrafts.WebSite.Services
{
    public class JsonFileProductsService
    {
        public JsonFileProductsService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json");

        public IEnumerable<Product> GetProducts()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            var json = jsonFileReader.ReadToEnd();
            var products = JsonSerializer.Deserialize<Product[]>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return products ?? Enumerable.Empty<Product>();
        }

        public void AddRating(string productId, int rating)
        {
            var products = GetProducts();

            if (products.First(x => x.Id == productId).Ratings == null)
            {
                products.First(x => x.Id == productId).Ratings = new int[] { rating };
            }
            else
            {
                var ratings = products.FirstOrDefault(x => x.Id == productId)?.Ratings?.ToList() ?? new List<int> { 0 };
                ratings.Add(rating);
                products.First(x => x.Id == productId).Ratings = ratings.ToArray();
            }

            using var outputStream = File.OpenWrite(JsonFileName);

            JsonSerializer.Serialize<IEnumerable<Product>>(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions
                {
                    SkipValidation = true,
                    Indented = true
                }),
                products
            );
        }
    }
}
