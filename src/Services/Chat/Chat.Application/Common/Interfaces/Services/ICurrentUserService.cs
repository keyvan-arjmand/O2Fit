namespace Chat.Application.Common.Interfaces.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Username { get; }
    string? FullName { get; }
    string? Language { get; }

}