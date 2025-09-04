using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FoodFacts.Messages;

namespace FoodFacts.ViewModels;

// Manages the state for the AppShell
public partial class AppShellViewModel : ObservableObject, IRecipient<UsernameChangedMessage>
{
    // Holds the current title for the main navigation bar.
    [ObservableProperty]
    private string _title;

    public AppShellViewModel()
    {
        // Sets the initial title and subscribes to username update messages.
        SetTitle(Settings.Username);
        WeakReferenceMessenger.Default.Register<UsernameChangedMessage>(this);
    }

    // Receives messages when the username is changed from the Settings page.
    public void Receive(UsernameChangedMessage message)
    {
        SetTitle(message.Value);
    }

    // Updates the Title property with a welcome message.
    private void SetTitle(string username)
    {
        Title = string.IsNullOrWhiteSpace(username) ? "Food Facts" : $"Welcome, {username}!";
    }
}