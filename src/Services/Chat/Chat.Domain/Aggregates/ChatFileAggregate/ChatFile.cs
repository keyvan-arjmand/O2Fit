namespace Chat.Domain.Aggregates.ChatFileAggregate;

public class ChatFile : AggregateRoot
{
    public ChatFile()
    {
        
    }
    public ChatFile(string fileName, string fileUrl)
    {
        FileName = fileName;
        FileUrl = fileUrl;
    }
    public string FileName { get; set; }
    public string FileUrl { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
}