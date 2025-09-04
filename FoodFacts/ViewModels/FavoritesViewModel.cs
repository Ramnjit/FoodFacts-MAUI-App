using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FoodFacts.Models;
using FoodFacts.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace FoodFacts.ViewModels;

// Manages the logic and data for the Favorites page.
public partial class FavoritesViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;

    // The "favorites" displayed in the UI.
    [ObservableProperty]
    private ObservableCollection<Product> _favorites;

    // Backup list of all favorites to make sorting easier.
    private List<Product> _allFavorites = new();

    // Current sort order for the list.
    [ObservableProperty]
    private SortOption _currentSortOrder = SortOption.None;

    // Text displayed on the sort button.
    public string SortButtonText => $"Sort: {CurrentSortOrder}";

    public FavoritesViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        _favorites = new ObservableCollection<Product>();
    }

    // Loads the list of favorite products from the local database.
    [RelayCommand]
    public async Task LoadFavoritesAsync()
    {
        var favoritesFromDb = await _databaseService.GetFavoritesAsync();

        _allFavorites.Clear();
        foreach (var favorite in favoritesFromDb)
        {
            _allFavorites.Add(favorite);
        }

        ApplySort();
    }

    // Navigates to the detail page for a selected product.
    [RelayCommand]
    private async Task GoToDetailsAsync(Product product)
    {
        if (product is null) return;

        await Shell.Current.GoToAsync(nameof(Views.DetailPage), true, new Dictionary<string, object>
        {
            { "id", product.Id }
        });
    }

    // Cycles through the available sort options.
    [RelayCommand]
    private void ChangeSortOrder()
    {        
        if (CurrentSortOrder == SortOption.None)
        {
            CurrentSortOrder = SortOption.AlphabeticalAsc;
        }
        else if (CurrentSortOrder == SortOption.AlphabeticalAsc)
        {
            CurrentSortOrder = SortOption.AlphabeticalDesc;
        }
        else
        {
            CurrentSortOrder = SortOption.None;
        }

        ApplySort();
        OnPropertyChanged(nameof(SortButtonText));
    }

    // Sorts and updates the public collection for the UI.
    private void ApplySort()
    {        
        List<Product> sortedFavorites;
        if (CurrentSortOrder == SortOption.AlphabeticalAsc)
        {
            sortedFavorites = _allFavorites.OrderBy(p => p.ProductName).ToList();
        }
        else if (CurrentSortOrder == SortOption.AlphabeticalDesc)
        {
            sortedFavorites = _allFavorites.OrderByDescending(p => p.ProductName).ToList();
        }
        else
        {
            sortedFavorites = _allFavorites;
        }

        Favorites.Clear();
        foreach (var favorite in sortedFavorites)
        {
            Favorites.Add(favorite);
        }
    }
}