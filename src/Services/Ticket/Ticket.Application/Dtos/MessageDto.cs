using MongoDB.Bson;
using Ticket.Domain.Enums;

namespace Ticket.Application.Dtos;

public class MessageDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageName { get; set; }
    public string Link { get; set; } = string.Empty;
    public LinkTarget Type { get; set; }
    public string? UserId { get; set; } // Message To User
    public Classification Classification { get; set; }
    public UserType UserType { get; set; }
    public bool? IsUserRead { get; set; }
    public bool IsGeneral { get; set; }
    public bool IsForce { get; set; }
}