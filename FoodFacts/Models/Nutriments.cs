using System.Text.Json.Serialization;

namespace FoodFacts.Models;

// This helper class represents the nested 'nutriments' object from the API's JSON response.
public class Nutriments
{
    // The [JsonPropertyName] attribute maps a C# property to the corresponding JSON field name.
    [JsonPropertyName("energy-kcal_100g")]
    public double Calories { get; set; }

    [JsonPropertyName("fat_100g")]
    public double Fat { get; set; }

    [JsonPropertyName("sugars_100g")]
    public double Sugars { get; set; }

    [JsonPropertyName("proteins_100g")]
    public double Proteins { get; set; }
}