namespace Notification.Application.Consumers;

public class PushedNotificationConsumer : IConsumer<PushedNotification>
{
    private readonly IMediator _mediator;
    private readonly ILogger<PushedNotificationConsumer> _logger;
    public PushedNotificationConsumer(IMediator mediator, ILogger<PushedNotificationConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PushedNotification> context)
    {
        var userInfo = await _mediator.Send(new GetUserDataFromPhoneBookQuery(context.Message.UserId));
        foreach (var fcmToken in userInfo.FcmTokens)
        {
            var message = new Message()
            {
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = context.Message.Title,
                    Body = context.Message.Body
                },
                Token = fcmToken
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
            if (string.IsNullOrEmpty(result))
            {
                _logger.LogError("notification not send to the with FCM: {FcmToken} and userId: {MessageUserId}", fcmToken, context.Message.UserId);  
            }
            else
            {
                await _mediator.Send(new CreateMessageLogCommand(context.Message.Body, fcmToken, string.Empty));
            }
        }
    }
}