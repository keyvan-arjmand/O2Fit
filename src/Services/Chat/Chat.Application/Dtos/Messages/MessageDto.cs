namespace Chat.Application.Dtos.Messages;

public class MessageDto
{
    public string Id { get; set; }
    public string SenderUserId { get; set; }
    public string SenderFullName { get; set; }

    public string RecipientUserId { get; set; }
    public string RecipientFullName { get; set; }

    public string Content { get; set; }
    public DateTime? DateRead { get; set; }
    public DateTime MessageSentDate { get; set; } 
    public MessageStatus Status { get; set; }
    
}