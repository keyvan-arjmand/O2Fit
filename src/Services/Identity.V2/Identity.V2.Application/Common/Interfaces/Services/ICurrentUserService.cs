namespace Identity.V2.Application.Common.Interfaces.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Username { get; }
}