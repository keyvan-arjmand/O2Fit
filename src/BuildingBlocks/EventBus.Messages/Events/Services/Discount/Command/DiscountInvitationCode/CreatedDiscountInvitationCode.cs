using Common.Enums.TypeEnums;

namespace EventBus.Messages.Events.Services.Discount.Command.DiscountInvitationCode;

public record CreatedDiscountInvitationCode : BaseEvent
{
    public string UserId { get; init; }
    public string Username { get; init; }
    public DiscountType DiscountType { get; init; }
}