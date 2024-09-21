using EventBus.Messages.Events.Services.Discount.Command.DiscountCurrencyRate;
using MassTransit;

namespace Currency.Application.EventHandler;

public class UpdateCurrencyRateDiscountEventHandler : INotificationHandler<UpdateCurrencyRate>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<UpdateCurrencyRateDiscountEventHandler> _logger;

    public UpdateCurrencyRateDiscountEventHandler(IPublishEndpoint publishEndpoint,
        ILogger<UpdateCurrencyRateDiscountEventHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Handle(UpdateCurrencyRate notification, CancellationToken cancellationToken)
    {
        try
        {
            await _publishEndpoint.Publish<UpdateCurrencyRate>(
                new UpdateCurrencyRate(
                    notification.Id,
                    notification.CountryIds,
                    notification.CurrencyCode,
                    notification.CoefficientCurrency
                ), cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}