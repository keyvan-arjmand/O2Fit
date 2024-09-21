using EventBus.Messages.Events.Services.Nutritionist.Order;
using MassTransit;

namespace Order.V2.Application.Orders.V1.EventHandler;

public class OrderedEventHandler : INotificationHandler<Ordered>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<Ordered> _logger;


    public OrderedEventHandler(IPublishEndpoint publishEndpoint, ILogger<Ordered> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Handle(Ordered notification, CancellationToken cancellationToken)
    {
        try
        {
            await _publishEndpoint.Publish<Ordered>(new Ordered
            {
                UserId = notification.UserId,
                NutritionistId = notification.NutritionistId,
                OrderId = notification.OrderId,
                PackageId = notification.PackageId
            }, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}