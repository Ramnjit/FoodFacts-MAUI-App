// File: Views/FavoritesPage.xaml.cs

using FoodFacts.ViewModels;

namespace FoodFacts.Views;

public partial class FavoritesPage : ContentPage
{
    private readonly FavoritesViewModel _viewModel;

    public FavoritesPage(FavoritesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    // This method runs every time the user navigates to this tab.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // We tell the ViewModel to refresh the list of favorites.
        await _viewModel.LoadFavoritesAsync();
    }
}