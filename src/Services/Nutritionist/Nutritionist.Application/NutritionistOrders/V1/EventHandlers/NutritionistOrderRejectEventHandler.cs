namespace Nutritionist.Application.NutritionistOrders.V1.EventHandlers;

public class NutritionistOrderRejectEventHandler : INotificationHandler<OrderRejected>
{
    private readonly ILogger<NutritionistOrderRejectEventHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public NutritionistOrderRejectEventHandler(ILogger<NutritionistOrderRejectEventHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(OrderRejected notification, CancellationToken cancellationToken)
    {
        try
        {
            await _publishEndpoint.Publish<OrderRejected>(new OrderRejected
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