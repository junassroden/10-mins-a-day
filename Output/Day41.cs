using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Day41
{
    // Model for API response
    public class Post
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; } = "";
        public string body { get; set; } = "";
    }

    // Handles API communication
    public class PostApiService
    {
        private readonly HttpClient _client;

        public PostApiService()
        {
            _client = new HttpClient();
        }

        // Gets all posts from the API
        public async Task<List<Post>> GetPostsAsync()
        {
            try
            {
                string url = "https://jsonplaceholder.typicode.com/posts";

                // Send GET request
                HttpResponseMessage response =
                    await _client.GetAsync(url);

                // Check if the request was successful
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"API returned status code: {response.StatusCode}");
                }

                // Read API response
                string json =
                    await response.Content.ReadAsStringAsync();

                // Convert JSON into C# objects
                List<Post>? posts =
                    JsonSerializer.Deserialize<List<Post>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                return posts ?? new List<Post>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nAPI ERROR:");
                Console.WriteLine(ex.Message);

                return new List<Post>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine("\nJSON ERROR:");
                Console.WriteLine(ex.Message);

                return new List<Post>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nUNEXPECTED ERROR:");
                Console.WriteLine(ex.Message);

                return new List<Post>();
            }
        }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Day 41 - API Analyzer";

            Console.WriteLine("========================================");
            Console.WriteLine("        DAY 41 - API ANALYZER");
            Console.WriteLine("========================================");

            int userId;

            // Ask the user for a valid user ID
            while (true)
            {
                Console.Write("\nEnter User ID (1-10): ");

                string? input = Console.ReadLine();

                // Validate the user's input
                if (int.TryParse(input, out userId) &&
                    userId >= 1 &&
                    userId <= 10)
                {
                    break;
                }

                Console.WriteLine(
                    "Invalid input. Please enter a number from 1 to 10.");
            }

            Console.WriteLine("\nFetching posts from API...");

            // Create API service
            PostApiService api = new PostApiService();

            // Get all posts from the API
            List<Post> allPosts =
                await api.GetPostsAsync();

            // Check if the API returned data
            if (allPosts.Count == 0)
            {
                Console.WriteLine(
                    "\nNo data was retrieved from the API.");

                return;
            }

            // Filter posts by user ID
            List<Post> userPosts = allPosts
                .Where(post => post.userId == userId)
                .ToList();

            // Check if the selected user has posts
            if (userPosts.Count == 0)
            {
                Console.WriteLine(
                    "\nNo posts were found for this user.");

                return;
            }

            // Find the post with the longest title
            Post longestTitle = userPosts
                .OrderByDescending(post => post.title.Length)
                .First();

            // Find the post with the longest body
            Post longestBody = userPosts
                .OrderByDescending(post => post.body.Length)
                .First();

            // Calculate average body length
            double averageBodyLength = userPosts
                .Average(post => post.body.Length);

            Console.WriteLine("\n========================================");
            Console.WriteLine("              RESULTS");
            Console.WriteLine("========================================");

            Console.WriteLine($"\nUser ID: {userId}");
            Console.WriteLine($"Total Posts: {userPosts.Count}");

            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("Longest Title");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine($"Post ID: {longestTitle.id}");
            Console.WriteLine($"Length: {longestTitle.title.Length}");
            Console.WriteLine($"Title: {longestTitle.title}");

            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("Longest Body");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine($"Post ID: {longestBody.id}");
            Console.WriteLine($"Length: {longestBody.body.Length}");
            Console.WriteLine($"Body: {longestBody.body}");

            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("Statistics");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine(
                $"Average Body Length: {averageBodyLength:F2} characters");

            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("Posts");
            Console.WriteLine("----------------------------------------");

            // Display every post belonging to the selected user
            foreach (Post post in userPosts)
            {
                Console.WriteLine(
                    $"[{post.id}] {post.title}");
            }

            Console.WriteLine("\n========================================");
            Console.WriteLine("             PROGRAM COMPLETE");
            Console.WriteLine("========================================");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
