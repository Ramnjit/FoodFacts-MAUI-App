// File: Views/SearchPage.xaml.cs

using FoodFacts.ViewModels;

namespace FoodFacts.Views;

public partial class SearchPage : ContentPage
{
    // We change the constructor to accept our ViewModel as a parameter.
    // The dependency injection system we just configured will automatically
    // provide the SearchViewModel instance when this page is created.
    public SearchPage(SearchViewModel viewModel)
    {
        InitializeComponent();

        // We set the page's "brain" to be the ViewModel instance.
        // This is the crucial link that makes MVVM work.
        BindingContext = viewModel;
    }
}