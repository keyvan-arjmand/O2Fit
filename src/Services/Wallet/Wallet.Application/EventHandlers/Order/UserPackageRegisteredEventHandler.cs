using EventBus.Messages.Events.Services.Identity.Package;
using EventBus.Messages.Events.Services.Wallet;
using MassTransit;

namespace Wallet.Application.EventHandlers.Order;

public class UserPackageRegisteredEventHandler : INotificationHandler<UserPackageRegistered>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<UserPackageRegisteredEventHandler> _logger;


    public UserPackageRegisteredEventHandler(IPublishEndpoint publishEndpoint, ILogger<UserPackageRegisteredEventHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Handle(UserPackageRegistered notification, CancellationToken cancellationToken)
    {
        try
        {
            await _publishEndpoint.Publish<UserPackageRegistered>(new UserPackageRegistered
            {
                ExpireDate = notification.ExpireDate,
                PackageType = notification.PackageType,
                UserId = notification.UserId
            }, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}