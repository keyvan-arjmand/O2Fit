using Common.Enums.TypeEnums;

namespace EventBus.Messages.Events.Services.Wallet;

public record CreatedWalletForUser : BaseEvent
{
    //when user Create profile
    public string UserId { get; init; }
    public string UserName { get; init; }
    public string CurrencyCode { get; init; }
    public int CountryId { get; init; }
    public UserType UserType { get; init; }
}