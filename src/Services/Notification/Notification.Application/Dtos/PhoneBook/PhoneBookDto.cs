namespace Notification.Application.Dtos.PhoneBook;

public class PhoneBookDto : IDto
{
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public List<string> FcmTokens { get; set; } = new();
    
}