using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;

namespace FoodFacts;

// Defines this class as the main entry point for the Android app.
[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{    
    public static MainActivity Instance { get; private set; }

    // Called when the app is first created.
    protected override void OnCreate(Bundle savedInstanceState)
    {
        Instance = this;
        base.OnCreate(savedInstanceState);

        // Handle the intent that launched the app (from a notification tap).
        HandleIntent(this.Intent);

        // For Android 13+, explicitly ask the user for permission to send notifications.
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
        {
            if (ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.PostNotifications) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Android.Manifest.Permission.PostNotifications }, 0);
            }
        }
    }

    // Called when the app is already running and receives a new intent.
    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);

        // Handles a new intent (from a second notification tap).
        HandleIntent(intent);
    }

    // Helper method to process incoming intents and navigate the app.
    private void HandleIntent(Intent intent)
    {
        // Check if the intent contains our custom "target_page" data.
        if (intent.HasExtra("target_page"))
        {
            var route = intent.GetStringExtra("target_page");

            // Navigate to the specified page on the main UI thread.
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Shell.Current.GoToAsync(route, true);
            });
        }
    }
}