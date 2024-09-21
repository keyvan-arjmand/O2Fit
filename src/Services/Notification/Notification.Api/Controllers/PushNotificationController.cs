using FirebaseAdmin.Messaging;
using MediatR;
using Notification.Application.Dtos;
using Notification.Application.Dtos.PhoneBook;
using Notification.Application.PhoneBooks.V1.Commands.CreateContact;
using Notification.Application.PhoneBooks.V1.Queries.GetAllContacts;

namespace Notification.Api.Controllers;

[ApiVersion("1")]
public class PushNotificationController : BaseApiController
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;
    private readonly ISmsService _smsService;
    private readonly ILogger<PushNotificationController> _logger;
    public PushNotificationController(ICurrentUserService currentUserService, IMediator mediator,
        ISmsService smsService, ILogger<PushNotificationController> logger)
    {
        _currentUserService = currentUserService;
        _mediator = mediator;
        _smsService = smsService;
        _logger = logger;
    }

    [HasPermission(PermissionsConstants.CreatePermission)]
    [HttpPost("send-message")]
    public async Task<ActionResult> SendMessage([FromBody] MessageRequestDto dto)
    {
        var message = new Message()
        {
            Notification = new FirebaseAdmin.Messaging.Notification
            {
                Title = dto.Title,
                Body = dto.Body
            },
            //Data = new Dictionary<string, string>()
            //{
            //    ["CustomData"] = "Hello, how are you doing?"
            //},
            Token = dto.DeviceToken
        };
        var messaging = FirebaseMessaging.DefaultInstance;
        var result = await messaging.SendAsync(message);

        if (!string.IsNullOrEmpty(result))
        {
            _logger.LogInformation("message sent");
            return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
        }
        else
        {
            throw new Exception("Error sending the message");
        }
    }

    [HasPermission(PermissionsConstants.CreatePermission)]
    [HttpPost("create-contact")]
    public async Task<ActionResult> CreateContact([FromBody] CreateContactDto dto, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CreateContactCommand(_currentUserService.UserId!, _currentUserService.Username!,
                _currentUserService.Fullname,
                !string.IsNullOrEmpty(_currentUserService.NutritionistProfileImageName), dto.FcmToken),
            cancellationToken);

        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreatePermission)]
    [HttpPost("push-notification-to-all-users")]
    public async Task<ActionResult> PushNotificationToAllUsers([FromBody] SendMessageToAllDto dto,
        CancellationToken cancellationToken)
    {
        var allUsers = await _mediator.Send(new GetAllContactsQuery(), cancellationToken);
        foreach (var phoneBookDto in allUsers)
        {
            foreach (var fcmToken in phoneBookDto.FcmTokens)
            {
                var message = new Message()
                {
                    Notification = new FirebaseAdmin.Messaging.Notification
                    {
                        Title = dto.Title,
                        Body = dto.Body
                    },
                    Token = fcmToken
                };
                var messaging = FirebaseMessaging.DefaultInstance;
                var result = await messaging.SendAsync(message, cancellationToken);
            }
        }

        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}