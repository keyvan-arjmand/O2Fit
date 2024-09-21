namespace Identity.Application.Common.Interface;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Username { get; }
}