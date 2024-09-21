using Microsoft.AspNetCore.Http;

namespace Chat.Application.Dtos.Messages;

public class AddFileToChatDto : IDto
{
    public AddFileToChatDto()
    {
        
    }

    public AddFileToChatDto(string orderId, IFormFile file)
    {
        OrderId = orderId;
        File = file;
    }
    public string OrderId { get; set; }
    public IFormFile File { get; set; }
}