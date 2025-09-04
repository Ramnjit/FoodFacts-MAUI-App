using SQLite;
using System.Text.Json.Serialization;

namespace FoodFacts.Models;

// Represents a single food product from the API and local database.
public class Product
{
    // [PrimaryKey] attribute defines the unique ID for the database table.
    // [JsonPropertyName] attribute maps this C# property to the JSON field from the API.
    [PrimaryKey]
    [JsonPropertyName("code")]
    public string Id { get; set; }

    [JsonPropertyName("product_name")]
    public string ProductName { get; set; }

    [JsonPropertyName("brands")]
    public string Brands { get; set; }

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; }

    // Flattened properties for database storage and UI binding.
    public double Calories { get; set; }
    public double Fat { get; set; }
    public double Sugars { get; set; }
    public double Proteins { get; set; }

    // [Ignore] attribute tells SQLite to not save this property to the database.
    [Ignore]
    [JsonPropertyName("ingredients_text")]
    public string Ingredients { get; set; }

    private Nutriments _nutriments;

    // "Bridge" property: reads the nested 'nutriments' JSON object.
    [Ignore]
    [JsonPropertyName("nutriments")]
    public Nutriments Nutriments
    {
        get => _nutriments;
        set
        {
            _nutriments = value;
            // When set, it "flattens" the data into the simple properties above.
            if (value != null)
            {
                Calories = value.Calories;
                Fat = value.Fat;
                Sugars = value.Sugars;
                Proteins = value.Proteins;
            }
        }
    }
}