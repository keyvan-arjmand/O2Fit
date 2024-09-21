namespace Chat.Application.Dtos.ChatFiles;

public class ChatFileDto : IDto
{
    public ChatFileDto()
    {
        
    }

    public ChatFileDto(string id, string fileName, string fileUrl, DateTime created)
    {
        Id = id;
        FileName = fileName;
        FileUrl = fileUrl;
        Created = created;
    }
    public string Id { get; set; }
    public string FileName { get; set; }
    public string FileUrl { get; set; }
    public DateTime Created { get; set; }
}