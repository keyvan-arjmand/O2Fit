using EventBus.Messages.Events.Services.Identity;

namespace Identity.V2.Application.UserProfiles.V1.EventHandlers;

public class UserProfileCreatedEventHandler : INotificationHandler<UserProfileCreated>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<UserProfileCreatedEventHandler> _logger;

    public UserProfileCreatedEventHandler(IPublishEndpoint publishEndpoint, ILogger<UserProfileCreatedEventHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Handle(UserProfileCreated notification, CancellationToken cancellationToken)
    {
        try
        {
            await _publishEndpoint.Publish<UserProfileCreated>(new
            {
                UserId = notification.UserId
            }, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}