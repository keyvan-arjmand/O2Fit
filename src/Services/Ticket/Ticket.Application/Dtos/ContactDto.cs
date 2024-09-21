namespace Ticket.Application.Dtos;

public class ContactDto
{
    public string Message { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime DateTime { get; set; } 
}