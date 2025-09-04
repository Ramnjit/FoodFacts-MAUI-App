using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using FoodFacts.Services;

namespace FoodFacts.Platforms.Android;

// Native Android implementation for showing notifications.
public class NotificationService : INotificationService
{
    // Required for Android 8.0+ notification channels.
    private const string ChannelId = "foodfacts_notifications";
    private const string ChannelName = "FoodFacts Notifications";

    public void ShowNotification(string title, string message, int notificationId, string targetPage)
    {
        var context = MainActivity.Instance;

        // Create an Intent to launch the app when the notification is tapped.
        var intent = new Intent(context, Java.Lang.Class.FromType(typeof(MainActivity)));
        intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);

        // Add the navigation route as extra data to the intent.
        intent.PutExtra("target_page", targetPage);
        intent.PutExtra("notification_id", notificationId);

        // Wrap the intent in a PendingIntent for the OS to use.
        var pendingIntent = PendingIntent.GetActivity(context, notificationId, intent, PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent);

        // Create a notification channel for Android 8.0+.
        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channel = new NotificationChannel(ChannelId, ChannelName, NotificationImportance.Default);
            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        // Build the notification's content and appearance.
        var builder = new NotificationCompat.Builder(context, ChannelId)
            .SetContentTitle(title)
            .SetContentText(message)
            .SetSmallIcon(Resource.Drawable.ic_stat_local_pizza)
            .SetContentIntent(pendingIntent) // Set the action to perform on tap.
            .SetAutoCancel(true); // Dismiss the notification on tap.

        // Display the notification.
        var notificationManagerCompat = NotificationManagerCompat.From(context);
        notificationManagerCompat.Notify(notificationId, builder.Build());
    }
}