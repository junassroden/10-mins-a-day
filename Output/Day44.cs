using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Day45
{
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; } = "";
        public double price { get; set; }
        public int stock { get; set; }
        public double rating { get; set; }
        public string category { get; set; } = "";
        public string description { get; set; } = "";
    }

    public class ProductResponse
    {
        public List<Product> products { get; set; } = new();
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public double Total => Product.price * Quantity;
    }

    class Program
    {
        static List<Product> products = new();
        static List<CartItem> cart = new();

        static async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                using HttpClient client = new();

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

        static void ShowProducts()
        {
            Console.WriteLine("\n========== PRODUCTS ==========");

            foreach (Product product in products)
            {
                Console.WriteLine(
                    $"{product.id}. {product.title} - ${product.price:F2} - Stock: {product.stock}"
                );
            }
        }

        static void SearchProduct()
        {
            Console.Write("\nSearch: ");
            string search = Console.ReadLine() ?? "";

            List<Product> results = products
                .Where(p => p.title.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            foreach (Product product in results)
            {
                Console.WriteLine(
                    $"{product.id}. {product.title} - ${product.price:F2}"
                );
            }
        }

        static void ProductDetails()
        {
            Console.Write("\nEnter Product ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            Product? product = products.FirstOrDefault(p => p.id == id);

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.WriteLine($"\nProduct: {product.title}");
            Console.WriteLine($"Price: ${product.price:F2}");
            Console.WriteLine($"Stock: {product.stock}");
            Console.WriteLine($"Rating: {product.rating}");
            Console.WriteLine($"Category: {product.category}");
            Console.WriteLine($"Description: {product.description}");
        }

        static void AddToCart()
        {
            Console.Write("\nEnter Product ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            Product? product = products.FirstOrDefault(p => p.id == id);

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write("Quantity: ");

            if (!int.TryParse(Console.ReadLine(), out int quantity) ||
                quantity <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            CartItem? existing = cart
                .FirstOrDefault(c => c.Product.id == id);

            int currentQuantity = existing?.Quantity ?? 0;

            if (currentQuantity + quantity > product.stock)
            {
                Console.WriteLine("Not enough stock.");
                return;
            }

            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem(product, quantity));
            }

            Console.WriteLine("Product added to cart.");
        }

        static void ViewCart()
        {
            Console.WriteLine("\n========== CART ==========");

            if (cart.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            foreach (CartItem item in cart)
            {
                Console.WriteLine(
                    $"{item.Product.title} x{item.Quantity} = ${item.Total:F2}"
                );
            }

            Console.WriteLine(
                $"Subtotal: ${cart.Sum(c => c.Total):F2}"
            );
        }

        static void RemoveFromCart()
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            Console.Write("\nEnter Product ID to remove: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            CartItem? item = cart
                .FirstOrDefault(c => c.Product.id == id);

            if (item == null)
            {
                Console.WriteLine("Product is not in cart.");
                return;
            }

            cart.Remove(item);

            Console.WriteLine("Product removed.");
        }

        static void Checkout()
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            double subtotal = cart.Sum(c => c.Total);
            double tax = subtotal * 0.12;
            double total = subtotal + tax;

            Console.WriteLine("\n========== CHECKOUT ==========");
            Console.WriteLine($"Subtotal: ${subtotal:F2}");
            Console.WriteLine($"Tax: ${tax:F2}");
            Console.WriteLine($"Total: ${total:F2}");

            Console.Write("\nConfirm checkout? (Y/N): ");
            string answer = Console.ReadLine() ?? "";

            if (answer.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                foreach (CartItem item in cart)
                {
                    item.Product.stock -= item.Quantity;
                }

                cart.Clear();

                Console.WriteLine("Order completed successfully.");
            }
            else
            {
                Console.WriteLine("Checkout cancelled.");
            }
        }

        static async Task Main()
        {
            Console.WriteLine("Loading products...");

            products = await GetProductsAsync();

            if (products.Count == 0)
            {
                Console.WriteLine("Unable to load products.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\n================================");
                Console.WriteLine("       MINI STORE SYSTEM");
                Console.WriteLine("================================");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Search Product");
                Console.WriteLine("3. Product Details");
                Console.WriteLine("4. Add to Cart");
                Console.WriteLine("5. View Cart");
                Console.WriteLine("6. Remove from Cart");
                Console.WriteLine("7. Checkout");
                Console.WriteLine("8. Exit");

                Console.Write("\nChoose: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        ShowProducts();
                        break;

                    case "2":
                        SearchProduct();
                        break;

                    case "3":
                        ProductDetails();
                        break;

                    case "4":
                        AddToCart();
                        break;

                    case "5":
                        ViewCart();
                        break;

                    case "6":
                        RemoveFromCart();
                        break;

                    case "7":
                        Checkout();
                        break;

                    case "8":
                        Console.WriteLine("Goodbye.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
