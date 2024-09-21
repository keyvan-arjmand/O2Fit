namespace Notification.Application.Consumers;

public class NutritionistCheckoutNotificationConsumer : IConsumer<NutritionistCheckoutNotification>
{
    private readonly ISmsService _smsService;
    private readonly IMediator _mediator;
    private readonly ILogger<NutritionistCheckoutNotificationConsumer> _logger;
    public NutritionistCheckoutNotificationConsumer(ISmsService smsService, IMediator mediator, ILogger<NutritionistCheckoutNotificationConsumer> logger)
    {
        _smsService = smsService;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NutritionistCheckoutNotification> context)
    {
        var userInfo = await _mediator.Send(new GetUserDataFromPhoneBookQuery(context.Message.UserId));
        
        var textMessage = string.IsNullOrEmpty(userInfo.FullName)
            ? "متخصص گرامی مبلغ " + context.Message.Amount + "از کیف پول شما کم شد و به حساب شما واریز شد"
            : $"{userInfo.FullName}" + " جان از کیف پول شما مبلغ " + context.Message.Amount +
              " کم شد و به حساب شما واریز شد";
        
        //send sms
        await _smsService.SendSmsAsync(userInfo.Username, "تسویه کیف پول" ,textMessage);
        await _mediator.Send(new CreateMessageLogCommand(textMessage, string.Empty, userInfo.Username));
        foreach (var fcmToken in userInfo.FcmTokens)
        {
            var message = new Message()
            {
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = "تسویه کیف پول",
                    Body = textMessage
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
                await _mediator.Send(new CreateMessageLogCommand(textMessage, fcmToken, string.Empty));
            }
        }
    }
}