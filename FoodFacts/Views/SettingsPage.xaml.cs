namespace FoodFacts.Views;
using FoodFacts.ViewModels;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
    }
}