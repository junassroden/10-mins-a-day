using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Day42
{
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; } = "";
        public double price { get; set; }
        public string category { get; set; } = "";
        public double rating { get; set; }
        public int stock { get; set; }
    }

    public class ProductResponse
    {
        public List<Product> products { get; set; } = new();
    }

    public class ApiService
    {
        private readonly HttpClient client = new HttpClient();

        // Get products from API
        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                string json = await client.GetStringAsync(
                    "https://dummyjson.com/products"
                );

                ProductResponse? data =
                    JsonSerializer.Deserialize<ProductResponse>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                return data?.products ?? new List<Product>();
            }
            catch (Exception error)
            {
                Console.WriteLine("API Error: " + error.Message);
                return new List<Product>();
            }
        }
    }

    class Program
    {
        static async Task Main()
        {
            ApiService api = new ApiService();
            List<Product> products = await api.GetProductsAsync();

            if (products.Count == 0)
                return;

            Console.WriteLine($"Total Products: {products.Count}");

            // Find cheapest product
            Product cheapest = products
                .OrderBy(p => p.price)
                .First();

            // Find most expensive product
            Product expensive = products
                .OrderByDescending(p => p.price)
                .First();

            // Calculate average price
            double average = products.Average(p => p.price);

            Console.WriteLine($"Cheapest: {cheapest.title} - ${cheapest.price}");
            Console.WriteLine($"Most Expensive: {expensive.title} - ${expensive.price}");
            Console.WriteLine($"Average Price: ${average:F2}");

            Console.Write("\nEnter category: ");
            string category = Console.ReadLine() ?? "";

            // Filter products by category
            List<Product> results = products
                .Where(p => p.category.Equals(
                    category,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            Console.WriteLine("\nProducts:");

            foreach (Product product in results)
            {
                Console.WriteLine(
                    $"{product.title} | ${product.price} | Stock: {product.stock}");
            }
        }
    }
}
