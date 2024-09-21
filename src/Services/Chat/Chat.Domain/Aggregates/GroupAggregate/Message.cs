namespace Chat.Domain.Aggregates.GroupAggregate;

public class Message : BaseEntity
{
    public Message()
    {
        
    }

    public Message(ObjectId senderUserId, string senderFullName, ObjectId recipientUserId, string recipientFullName, string content)
    {
        SenderUserId = senderUserId;
        SenderFullName = senderFullName;
        RecipientUserId = recipientUserId;
        RecipientFullName = recipientFullName;
        Content = content;
    }
    public ObjectId SenderUserId { get; set; }
    public string SenderFullName { get; set; }

    public ObjectId RecipientUserId { get; set; }
    public string RecipientFullName { get; set; }

    public string Content { get; set; }
    public DateTime? DateRead { get; set; }
    public DateTime MessageSentDate { get; set; } = DateTime.UtcNow;
    public MessageStatus Status { get; set; } = MessageStatus.Send;
}