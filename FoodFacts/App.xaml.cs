namespace FoodFacts;

using MauiAppTheme = Microsoft.Maui.ApplicationModel.AppTheme;

public partial class App : Application
{    
    // Receives the 'AppShell' through Dependency Injection.
    public App(AppShell shell)
    {
        InitializeComponent();

        // Set the main UI of the application to be the AppShell instance.
        MainPage = shell;

        // Logic for applying user selected theme read from user preferences        
        if (Settings.Theme == AppTheme.Light)
        {
            Application.Current.UserAppTheme = MauiAppTheme.Light;
        }
        else if (Settings.Theme == AppTheme.Dark)
        {
            Application.Current.UserAppTheme = MauiAppTheme.Dark;
        }
        else
        {
            // 'Unspecified' tells the app to follow the phone's system theme (light or dark).
            Application.Current.UserAppTheme = MauiAppTheme.Unspecified;
        }
    }
}