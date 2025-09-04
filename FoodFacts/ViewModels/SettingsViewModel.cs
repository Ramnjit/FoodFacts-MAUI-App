using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FoodFacts.Messages;
using System.Linq;

namespace FoodFacts.ViewModels;

public partial class SettingsViewModel : ObservableObject
{ 
    [ObservableProperty]
    private string _username = Settings.Username;
 
    [ObservableProperty]
    private AppTheme _theme = Settings.Theme;

    // Provides the list of theme options for the Picker control in the UI.
    public List<AppTheme> ThemeOptions { get; } = Enum.GetValues(typeof(AppTheme)).Cast<AppTheme>().ToList();

    // Saves the current settings to the device and broadcasts a message that the username changed.
    [RelayCommand]
    private void SaveSettings()
    {
        Settings.Username = Username;
        Settings.Theme = Theme;
        WeakReferenceMessenger.Default.Send(new UsernameChangedMessage(Username));
    }
}