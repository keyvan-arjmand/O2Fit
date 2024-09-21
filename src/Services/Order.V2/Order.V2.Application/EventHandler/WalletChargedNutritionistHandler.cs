using EventBus.Messages.Events.Services.Wallet;
using MassTransit;

namespace Order.V2.Application.EventHandler;

public class WalletChargedNutritionistHandler : INotificationHandler<WalletChargedNutritionistEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<WalletChargedNutritionistHandler> _logger;

    public WalletChargedNutritionistHandler(IPublishEndpoint publishEndpoint, ILogger<WalletChargedNutritionistHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Handle(WalletChargedNutritionistEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            await _publishEndpoint.Publish<WalletChargedNutritionistEvent>(new WalletChargedNutritionistEvent
            {
                TransactionId = notification.TransactionId,
                NutritionistId = notification.NutritionistId,
                OrderId = notification.OrderId,
            }, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}