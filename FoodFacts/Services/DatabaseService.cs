using FoodFacts.Models;
using SQLite;

namespace FoodFacts.Services;

// Handles all communication with the local SQLite database.
public class DatabaseService
{
    // Holds the asynchronous connection to the database.
    private SQLiteAsyncConnection _database;

    // Initializes the database connection and creates the Product table if it doesn't exist.   
    private async Task Init()
    {
        if (_database is not null)
            return;

        // Gets a safe path for the database file.
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "FoodFacts.db");

        _database = new SQLiteAsyncConnection(databasePath);
        await _database.CreateTableAsync<Product>();
    }

    // Gets all products saved in the favorites table.
    public async Task<List<Product>> GetFavoritesAsync()
    {
        await Init();
        return await _database.Table<Product>().ToListAsync();
    }

    // Checks if a product with a specific ID is already in the database.
    public async Task<bool> IsFavoriteAsync(string productId)
    {
        await Init();
        var product = await _database.Table<Product>().Where(p => p.Id == productId).FirstOrDefaultAsync();
        return product != null;
    }

    // Adds a new product to the favorites table.
    public async Task AddFavoriteAsync(Product product)
    {
        await Init();
        await _database.InsertAsync(product);
    }

    // Removes a product from the favorites table.
    public async Task RemoveFavoriteAsync(Product product)
    {
        await Init();
        await _database.DeleteAsync(product);
    }
}