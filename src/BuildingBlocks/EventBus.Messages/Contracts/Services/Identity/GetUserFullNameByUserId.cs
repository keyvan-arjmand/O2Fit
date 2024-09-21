namespace EventBus.Messages.Contracts.Services.Identity;

public record GetUserFullNameByUserId
{
    public string UserId { get; init; }
}