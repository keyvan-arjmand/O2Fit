namespace Chat.Application.Dtos.Messages;

public class CreateMessageDto : IDto
{
    public CreateMessageDto()
    {
        
    }

    public CreateMessageDto(string groupName, string content)
    {
        GroupName = groupName;
        Content = content;
    }
    public string GroupName { get; set; }
    public string Content { get; set; }
}