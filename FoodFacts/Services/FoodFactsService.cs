using FoodFacts.Models;
using System.Text.Json;
using System.Diagnostics;

namespace FoodFacts.Services;

// Handles all communication with the live Open Food Facts API.
public class FoodFactsService
{
    // HttpClient is the main class used to send and receive HTTP requests.
    private readonly HttpClient _httpClient;
    
    private const string BaseUrl = "https://world.openfoodfacts.org";

    public FoodFactsService()
    {
        _httpClient = new HttpClient();
    }

    // Searches for a list of products based on a search term.
    public async Task<List<Product>> SearchProductsAsync(string searchTerm)
    {
        var url = $"{BaseUrl}/cgi/search.pl?search_terms={searchTerm}&search_simple=1&action=process&json=1";

        try
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var searchResponse = JsonSerializer.Deserialize<SearchResponse>(json);
                return searchResponse?.Products ?? new List<Product>();
            }
        }
        catch (Exception ex)
        {            
            Debug.WriteLine($"Error fetching food facts: {ex.Message}");
        }

        // If request failed or an error occurred, return an empty list.
        return new List<Product>();
    }

    // Fetches a single product by its unique ID
    public async Task<Product> GetProductByIdAsync(string id)
    {
        var url = $"{BaseUrl}/api/v2/product/{id}.json";
        try
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var getProductResponse = JsonSerializer.Deserialize<GetProductResponse>(json);
                return getProductResponse?.Product;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching product by id: {ex.Message}");
        }

        // If request fails or the product is not found, return null.
        return null;
    }
}