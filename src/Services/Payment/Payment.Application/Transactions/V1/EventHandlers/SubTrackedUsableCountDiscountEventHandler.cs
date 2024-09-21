using EventBus.Messages.Events.Services.Discount.Command.SubTrackUsableCountDiscount;
using MassTransit;

namespace Payment.Application.Transactions.V1.EventHandlers;

public class SubTrackedUsableCountDiscountEventHandler : INotificationHandler<SubTrackedUsableCountDiscountO2FitEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<SubTrackedUsableCountDiscountEventHandler> _logger;


    public SubTrackedUsableCountDiscountEventHandler(IPublishEndpoint publishEndpoint,
        ILogger<SubTrackedUsableCountDiscountEventHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }


    public async Task Handle(SubTrackedUsableCountDiscountO2FitEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            // await _publishEndpoint.Publish<SubTrackedUsableCountDiscountEvent>(new SubTrackedUsableCountDiscountEvent
            // {
            //     DiscountCode = notification.DiscountCode,
            //     Username = notification.Username,
            //     UserId = notification.UserId
            // }, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}