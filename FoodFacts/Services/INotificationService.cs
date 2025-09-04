namespace FoodFacts.Services;

public interface INotificationService
{
    // Defines the contract for a platform-specific notification service.
    // Any class that implements this interface must provide this method.
    void ShowNotification(string title, string message, int notificationId, string targetPage);
}