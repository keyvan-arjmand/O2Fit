namespace Nutritionist.Application.NutritionistOrders.V1.EventHandlers;

public class NutritionistOrderCreatedEventHandler : INotificationHandler<NutritionistOrderCreated>
{
    private readonly ILogger<NutritionistOrderCreatedEventHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public NutritionistOrderCreatedEventHandler(ILogger<NutritionistOrderCreatedEventHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(NutritionistOrderCreated notification, CancellationToken cancellationToken)
    {
        try
        {
            await _publishEndpoint.Publish<NutritionistOrderCreated>(new NutritionistOrderCreated
            {
                NutritionistId = notification.NutritionistId,
                PackageNameArabic = notification.PackageNameArabic,
                PackageNameEnglish = notification.PackageNameEnglish,
                PackageNamePersian = notification.PackageNamePersian
            },cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}