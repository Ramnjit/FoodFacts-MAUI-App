using FoodFacts.ViewModels;

namespace FoodFacts.Views;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailViewModel viewModel)
	{
		InitializeComponent();
        // This is the crucial line that "glues" the View to the ViewModel.
        BindingContext = viewModel;
    }
}