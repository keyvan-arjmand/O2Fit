namespace Chat.Application.Dtos.Messages;

public class MessageResultDto : IDto
{
    public MessageResultDto()
    {
        
    }

    public MessageResultDto(string senderUserId, string senderFullName, string recipientUserId, string recipientFullName, string content, DateTime? dateRead, DateTime messageSentDate, MessageStatus status)
    {
        SenderUserId = senderUserId;
        SenderFullName = senderFullName;
        RecipientUserId = recipientUserId;
        RecipientFullName = recipientFullName;
        Content = content;
        DateRead = dateRead;
        MessageSentDate = messageSentDate;
        Status = status;
    }
    public string SenderUserId { get; set; }
    public string SenderFullName { get; set; }
    public string RecipientUserId { get; set; }
    public string RecipientFullName { get; set; }
    public string Content { get; set; }
    public DateTime? DateRead { get; set; }
    public DateTime MessageSentDate { get; set; }
    public MessageStatus Status { get; set; }
}