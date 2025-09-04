using FoodFacts.Views;
using FoodFacts.ViewModels;

namespace FoodFacts;

public partial class AppShell : Shell
{    
    // Receives the AppShellViewModel via Dependency Injection.
    public AppShell(AppShellViewModel viewModel)
    {
        InitializeComponent();
        
        // Connects the TitleView in the XAML to the Title property in the ViewModel.
        BindingContext = viewModel;

        // Registers route for our DetailPage.
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
    }
}