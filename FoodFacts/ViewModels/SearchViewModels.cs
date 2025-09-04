using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FoodFacts.Models;
using FoodFacts.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace FoodFacts.ViewModels;

public partial class SearchViewModel : ObservableObject
{
    private readonly FoodFactsService _foodFactsService;

    [ObservableProperty]
    private string _searchText;

    [ObservableProperty]
    private ObservableCollection<Product> _products;

    [ObservableProperty]
    private bool _isSearching;

    [ObservableProperty]
    private SortOption _currentSortOrder = SortOption.None;

    public string SortButtonText => $"Sort: {CurrentSortOrder}";

    public SearchViewModel(FoodFactsService foodFactsService)
    {
        _foodFactsService = foodFactsService;
        _products = new ObservableCollection<Product>();
    }
    
    // Fetches products from the API based on the search text.
    [RelayCommand]
    private async Task SearchProductsAsync()
    {
        if (IsSearching || string.IsNullOrWhiteSpace(SearchText))
            return;

        try
        {
            IsSearching = true;

            var searchResult = await _foodFactsService.SearchProductsAsync(SearchText);

            // After getting results, apply the current sort order before displaying.
            var sortedResult = ApplySort(searchResult);

            Products.Clear();
            if (sortedResult is not null)
            {
                foreach (var product in sortedResult)
                {
                    Products.Add(product);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error searching for products: {ex.Message}");
        }
        finally
        {
            IsSearching = false;
        }
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

        // Re-sort the products currently being displayed.
        var currentlyDisplayedProducts = new List<Product>(Products);
        var sortedProducts = ApplySort(currentlyDisplayedProducts);

        Products.Clear();
        foreach (var product in sortedProducts)
        {
            Products.Add(product);
        }

        OnPropertyChanged(nameof(SortButtonText));
    }

    // Sorts a given list of products based on the current sort order.
    private List<Product> ApplySort(List<Product> products)
    {        
        if (CurrentSortOrder == SortOption.AlphabeticalAsc)
        {
            return products.OrderBy(p => p.ProductName).ToList();
        }
        else if (CurrentSortOrder == SortOption.AlphabeticalDesc)
        {
            return products.OrderByDescending(p => p.ProductName).ToList();
        }
        else
        {
            return products;
        }
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
}