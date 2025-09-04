using System.Text.Json.Serialization;

namespace FoodFacts.Models;

public class SearchResponse
{
    // API returns a list of products under a JSON key named "products".
    [JsonPropertyName("products")]
    public List<Product> Products { get; set; }
}