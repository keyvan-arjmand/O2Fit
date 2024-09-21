namespace Notification.Domain.Aggregates.MessageLogAggregate;

public class MessageLog : AggregateRoot
{
    public MessageLog()
    {
        
    }

    public MessageLog(string text, string toFcmToken, string toPhoneNumber)
    {
        Text = text;
        ToPhoneNumber = toPhoneNumber;
        ToFcmToken = toFcmToken;
    }

    public string Text { get; set; }
    public string ToPhoneNumber { get; set; }
    public string ToFcmToken { get; set; }
}