namespace Nutritionist.Application.NutritionistOrders.V1.EventHandlers;

public class NutritionistOrderAcceptedEventHandler : INotificationHandler<OrderAccepted>
{
    private readonly ILogger<NutritionistOrderAcceptedEventHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public NutritionistOrderAcceptedEventHandler(ILogger<NutritionistOrderAcceptedEventHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(OrderAccepted notification, CancellationToken cancellationToken)
    {
        try
        {
            await _publishEndpoint.Publish<OrderAccepted>(new OrderAccepted
            {
                Username = notification.Username,
                OrderId = notification.OrderId,
                UserId = notification.UserId
            },cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}