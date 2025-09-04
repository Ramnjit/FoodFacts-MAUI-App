using CommunityToolkit.Mvvm.Messaging.Messages;

namespace FoodFacts.Messages;

// Message carries the new username as a string.
public class UsernameChangedMessage : ValueChangedMessage<string>
{
    public UsernameChangedMessage(string value) : base(value)
    {
    }
}