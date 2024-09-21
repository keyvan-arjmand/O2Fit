namespace Notification.Domain.Aggregates.PhoneBookAggregate;

public class PhoneBook : AggregateRoot
{
    public PhoneBook()
    {
        
    }

    public PhoneBook(ObjectId userId, string username, string? fullName, bool isNutritionist)
    {
        UserId = userId;
        Username = username;
        FullName = fullName;
        IsNutritionist = isNutritionist;
    }
    public ObjectId UserId { get; set; }
    public string Username { get; set; } 
    public string? FullName { get; set; }
    public bool IsNutritionist { get; set; }
    public List<string> FcmTokens { get; set; } = new();
}