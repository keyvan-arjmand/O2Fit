using EventBus.Messages.Events.Services.Wallet;
using MassTransit;

namespace Payment.Application.Transactions.V1.EventHandlers;

public class WalletTransactionCreatedEventHandler : INotificationHandler<WalletTransactionCreatedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<WalletTransactionCreatedEventHandler> _logger;


    public WalletTransactionCreatedEventHandler(IPublishEndpoint publishEndpoint, ILogger<WalletTransactionCreatedEventHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Handle(WalletTransactionCreatedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            await _publishEndpoint.Publish<WalletTransactionCreatedEvent>(new WalletTransactionCreatedEvent
            {
                PaymentType = notification.PaymentType,
                UserType = notification.UserType,
                PackageId = notification.PackageId,
                TransactionId = notification.TransactionId
            }, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}