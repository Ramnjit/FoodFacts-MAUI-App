using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FoodFacts.Models;
using FoodFacts.Services;

namespace FoodFacts.ViewModels;

// This attribute tells Shell Navigation how to pass data to this ViewModel.
// It maps a navigation parameter named "id" to our "ProductId" property.
[QueryProperty(nameof(ProductId), "id")]
public partial class DetailViewModel : ObservableObject
{
    // private fields hold the services we get from Dependency Injection.
    private readonly INotificationService _notificationService;
    private readonly FoodFactsService _foodFactsService;
    private readonly DatabaseService _databaseService;
    
    // Holds the full product details for the UI to bind to.
    [ObservableProperty]
    private Product product;

    // Receives the product's ID from the navigation system.
    [ObservableProperty]
    private string productId;

    // Tracks if the current product is in the favorites database.
    [ObservableProperty]
    private bool isFavorite;

    public DetailViewModel(INotificationService notificationService, FoodFactsService foodFactsService, DatabaseService databaseService)
    {
        _notificationService = notificationService;
        _foodFactsService = foodFactsService;
        _databaseService = databaseService;
    }

    // This is auto-called by the MVVM Toolkit whenever 'ProductId' is set.
    async partial void OnProductIdChanged(string value)
    {
        await LoadProductAsync();
    }

    // Loads the full product data from the API and checks its favorite status.
    private async Task LoadProductAsync()
    {
        Product = await _foodFactsService.GetProductByIdAsync(ProductId);

        if (Product is not null)
        {
            IsFavorite = await _databaseService.IsFavoriteAsync(Product.Id);
        }
    }

    // Adds or removes the product from the local favorites database.
    [RelayCommand]
    private async Task ToggleFavoriteAsync()
    {
        if (Product is null) return;

        if (IsFavorite)
        {
            await _databaseService.RemoveFavoriteAsync(Product);
        }
        else
        {
            await _databaseService.AddFavoriteAsync(Product);
        }

        // Toggles the property to update the star icon in the UI.
        IsFavorite = !IsFavorite;
    }

    // Schedules a native Android notification.
    [RelayCommand]
    private void ScheduleNotification()
    {
        if (Product is null) return;

        string title = "Product Reminder";
        string message = $"Don't forget to check the ingredients for {Product.ProductName}!";
        int notificationId = (int)DateTime.Now.Ticks;
        string targetPage = $"{nameof(Views.DetailPage)}?id={Product.Id}";

        _notificationService.ShowNotification(title, message, notificationId, targetPage);
    }
}