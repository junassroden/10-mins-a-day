using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Day43
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public string username { get; set; } = "";
        public string email { get; set; } = "";
        public Address address { get; set; } = new();
        public Company company { get; set; } = new();
    }

    public class Address
    {
        public string city { get; set; } = "";
    }

    public class Company
    {
        public string name { get; set; } = "";
    }

    class Program
    {
        static async Task<List<User>> GetUsersAsync()
        {
            try
            {
                using HttpClient client = new();

                string json = await client.GetStringAsync(
                    "https://jsonplaceholder.typicode.com/users");

                return JsonSerializer.Deserialize<List<User>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<User>();
            }
            catch (Exception error)
            {
                Console.WriteLine("API Error: " + error.Message);
                return new List<User>();
            }
        }

        static async Task Main()
        {
            List<User> users = await GetUsersAsync();

            if (users.Count == 0)
                return;

            Console.WriteLine($"Total Users: {users.Count}");

            // Find the user with the longest name
            User longest = users
                .OrderByDescending(u => u.name.Length)
                .First();

            Console.WriteLine($"Longest Name: {longest.name}");

            // Display companies without duplicates
            Console.WriteLine("\nCompanies:");

            foreach (string company in users
                .Select(u => u.company.name)
                .Distinct())
            {
                Console.WriteLine(company);
            }

            Console.Write("\nEnter city: ");
            string city = Console.ReadLine() ?? "";

            // Filter users by city
            List<User> results = users
                .Where(u => u.address.city.Equals(
                    city,
                    StringComparison.OrdinalIgnoreCase))
                .OrderBy(u => u.name)
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No users found.");
                return;
            }

            Console.WriteLine($"\nUsers in {city}:");

            foreach (User user in results)
            {
                Console.WriteLine(
                    $"{user.name} | {user.email} | {user.company.name}");
            }
        }
    }
}