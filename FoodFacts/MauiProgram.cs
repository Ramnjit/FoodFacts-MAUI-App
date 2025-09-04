using Microsoft.Extensions.Logging;
using FoodFacts.Services;
using FoodFacts.Views;
using FoodFacts.ViewModels;

namespace FoodFacts;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                // Registers custom fonts for the app.
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register native services for specific platforms (Android).
#if ANDROID
        builder.Services.AddSingleton<INotificationService, Platforms.Android.NotificationService>();
#endif

        // Register app services for the Dependency Injection container.
        builder.Services.AddSingleton<FoodFactsService>();
        builder.Services.AddSingleton<DatabaseService>();

        // Register the main app shell and its viewmodel.
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<AppShellViewModel>();

        // Register all of the app's pages (Views) and their ViewModels.
        builder.Services.AddSingleton<SearchPage>();
        builder.Services.AddSingleton<SearchViewModel>();

        builder.Services.AddSingleton<DetailPage>();
        builder.Services.AddSingleton<DetailViewModel>();

        builder.Services.AddSingleton<FavoritesPage>();
        builder.Services.AddSingleton<FavoritesViewModel>();

        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<SettingsViewModel>();

        // Enable extra logging only when in Debug mode.
#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Create and return the configured app.
        return builder.Build();
    }
}