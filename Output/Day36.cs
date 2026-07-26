using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        List<Pokemon> pokemonList = await GetPokemonList();

        if (pokemonList == null)
        {
            Console.WriteLine("Unable to retrieve Pokémon data.");
            return;
        }

        Console.WriteLine("===== POKEMON =====");

        for (int i = 0; i < pokemonList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {pokemonList[i].Name}");
        }

        char choice;

        do
        {
            Console.Write("\nEnter Pokémon name: ");
            string name = Console.ReadLine().Trim().ToLower();

            await GetPokemonDetails(name);

            Console.Write("\nSearch again? (Y/N): ");
            choice = Console.ReadLine().ToUpper()[0];

        } while (choice == 'Y');
    }

    static async Task<List<Pokemon>> GetPokemonList()
    {
        try
        {
            string url = "https://pokeapi.co/api/v2/pokemon?limit=20";

            string json = await client.GetStringAsync(url);

            PokemonResponse response = JsonSerializer.Deserialize<PokemonResponse>(json);

            return response.Results;
        }
        catch
        {
            return null;
        }
    }

    static async Task GetPokemonDetails(string name)
    {
        try
        {
            string url = $"https://pokeapi.co/api/v2/pokemon/{name}";

            string json = await client.GetStringAsync(url);

            PokemonDetails pokemon = JsonSerializer.Deserialize<PokemonDetails>(json);

            Console.WriteLine("\n===== POKEMON DETAILS =====");
            Console.WriteLine($"Name: {pokemon.Name}");
            Console.WriteLine($"Height: {pokemon.Height}");
            Console.WriteLine($"Weight: {pokemon.Weight}");
            Console.WriteLine($"Base Experience: {pokemon.Base_Experience}");

            Console.WriteLine("\nAbilities:");
            foreach (var ability in pokemon.Abilities)
            {
                Console.WriteLine($"- {ability.Ability.Name}");
            }

            Console.WriteLine("\nTypes:");
            foreach (var type in pokemon.Types)
            {
                Console.WriteLine($"- {type.Type.Name}");
            }

            Console.WriteLine($"\nFront Sprite:");
            Console.WriteLine(pokemon.Sprites.Front_Default);
        }
        catch
        {
            Console.WriteLine("Pokémon not found.");
        }
    }
}

class PokemonResponse
{
    public List<Pokemon> Results { get; set; }
}

class Pokemon
{
    public string Name { get; set; }
    public string Url { get; set; }
}

class PokemonDetails
{
    public string Name { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }

    public int Base_Experience { get; set; }

    public List<AbilityWrapper> Abilities { get; set; }

    public List<TypeWrapper> Types { get; set; }

    public Sprite Sprites { get; set; }
}

class AbilityWrapper
{
    public Ability Ability { get; set; }
}

class Ability
{
    public string Name { get; set; }
}

class TypeWrapper
{
    public PokemonType Type { get; set; }
}

class PokemonType
{
    public string Name { get; set; }
}

class Sprite
{
    public string Front_Default { get; set; }
}
