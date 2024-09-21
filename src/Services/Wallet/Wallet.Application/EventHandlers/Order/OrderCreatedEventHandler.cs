using EventBus.Messages.Events.Services.Order;
using MassTransit;

namespace Wallet.Application.EventHandlers.Order;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<OrderCreatedEventHandler> _logger;

    public OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEventHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            await _publishEndpoint.Publish<OrderCreatedEvent>(new OrderCreatedEvent
            {
                PaymentTransactionId = notification.PaymentTransactionId,
                WalletTransactionId = notification.WalletTransactionId,
                UserId = notification.UserId,
                Username = notification.Username
            }
           , cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}