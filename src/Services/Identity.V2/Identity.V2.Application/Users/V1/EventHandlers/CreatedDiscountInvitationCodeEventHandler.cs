using EventBus.Messages.Events.Services.Discount.Command.DiscountInvitationCode;

namespace Identity.V2.Application.Users.V1.EventHandlers;

public class CreatedDiscountInvitationCodeEventHandler: INotificationHandler<CreatedDiscountInvitationCode>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<CreatedDiscountInvitationCodeEventHandler> _logger;

    public CreatedDiscountInvitationCodeEventHandler(IPublishEndpoint publishEndpoint, ILogger<CreatedDiscountInvitationCodeEventHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Handle(CreatedDiscountInvitationCode notification, CancellationToken cancellationToken)
    {
        try
        {
            await _publishEndpoint.Publish<CreatedDiscountInvitationCode>(new
            {
                UserId = notification.UserId,
                DiscountType = notification.DiscountType
            }, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}