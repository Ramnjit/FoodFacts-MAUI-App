using MauiAppTheme = Microsoft.Maui.ApplicationModel.AppTheme;

namespace FoodFacts;

public static class Settings
{    
    private const string UsernameKey = "username_key";
    private const string ThemeKey = "app_theme_key";

    public static string Username
    {
 
        // Retrieves username value from Preferences, provides default if it doesn't exist.
        get => Preferences.Get(UsernameKey, string.Empty);
        
        // Saves new username value to Preferences.
        set => Preferences.Set(UsernameKey, value);
    }

    public static AppTheme Theme
    {
        // Retrieves theme value from Preferences
        get => (AppTheme)Preferences.Get("app_theme_key", (int)AppTheme.System);

        // Saves theme value to Preferences.
        set
        {           
            Preferences.Set(ThemeKey, (int)value);
         
            if (value == AppTheme.Light)
            {
                Application.Current.UserAppTheme = MauiAppTheme.Light;
            }
            else if (value == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = MauiAppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = MauiAppTheme.Unspecified;
            }
        }
    }
}