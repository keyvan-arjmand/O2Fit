using EventBus.Messages.Events.Services.Wallet;
using MassTransit;

namespace Order.V2.Application.EventHandler
{
    public class WalletChargedUserEventHandler : INotificationHandler<WalletChargedUserEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<WalletChargedUserEventHandler> _logger;

        public WalletChargedUserEventHandler(IPublishEndpoint publishEndpoint, ILogger<WalletChargedUserEventHandler> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(WalletChargedUserEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await _publishEndpoint.Publish<WalletChargedUserEvent>(new WalletChargedUserEvent
                {
                    TransactionId = notification.TransactionId,
                    OrderId = notification.OrderId,
                }, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
