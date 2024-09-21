namespace Notification.Application.Consumers;

public class NutritionistOrderCreatedConsumer : IConsumer<NutritionistOrderCreated>
{
    private readonly IMediator _mediator;
    private readonly ILogger<NutritionistOrderCreatedConsumer> _logger;

    public NutritionistOrderCreatedConsumer(IMediator mediator, ILogger<NutritionistOrderCreatedConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NutritionistOrderCreated> context)
    {
        var userInfo = await _mediator.Send(new GetUserDataFromPhoneBookQuery(context.Message.NutritionistId));

        var textMessage = "متخصص گرامی پکیج شما با نام" + $" {context.Message.PackageNamePersian}" +
                          "خریداری شد، لطفا برای تایید وضعیت سفارش به پروفایل خود مراجعه کنید";
        foreach (var fcmToken in userInfo.FcmTokens)
        {
            var message = new Message()
            {
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = "در خواست رژیم جدید",
                    Body = textMessage
                },
                Token = fcmToken
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
            if (string.IsNullOrEmpty(result))
            {
                _logger.LogError("notification not send to the with FCM: {FcmToken} and userId: {MessageUserId}", fcmToken, context.Message.NutritionistId);  
            }
            else
            {
                await _mediator.Send(new CreateMessageLogCommand(textMessage, fcmToken, string.Empty));
            }
        }
    }
}