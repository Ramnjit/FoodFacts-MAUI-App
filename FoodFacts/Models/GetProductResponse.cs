using System.Text.Json.Serialization;

namespace FoodFacts.Models;

public class GetProductResponse
{
    // The API returns a single product object under a JSON key named "product".
    [JsonPropertyName("product")]
    public Product Product{ get; set; }
}