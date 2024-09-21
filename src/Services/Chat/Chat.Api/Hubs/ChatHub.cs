using Chat.Api.Extensions;
using Chat.Application.Common.Utilities;
using Chat.Application.Dtos.Groups;
using Chat.Application.Dtos.Messages;
using Chat.Application.Groups.V1.Commands.AddConnectionToGroup;
using Chat.Application.Groups.V1.Commands.RemoveConnectionFromGroup;
using Chat.Application.Groups.V1.Queries.GetGroupByName;
using Chat.Application.Messages.V1.Commands.CreateMessage;
using Chat.Application.Messages.V1.Commands.ReadAllUnReadMessages;
using Chat.Domain.Enums;
using Common.Enums;
using EventBus.Messages.Events.Services.Notification;

namespace Chat.Api.Hubs;

[HasPermission(PermissionsConstants.Chat)]
public class ChatHub : Hub
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;
    private readonly ILogger<ChatHub> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    public ChatHub(ICurrentUserService currentUserService, IMediator mediator, ILogger<ChatHub> logger, IPublishEndpoint publishEndpoint)
    {
        _currentUserService = currentUserService;
        _mediator = mediator;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var orderId = Context.GetHttpContext().Request.Query["orderId"].ToString();
        var groupInfo = await _mediator.Send(new GetGroupByNameQuery(orderId));

        var otherUserId = _currentUserService.UserId == groupInfo.NutritionistId ? groupInfo.UserId : groupInfo.NutritionistId;
        
        if (_currentUserService.UserId == groupInfo.UserId || _currentUserService.UserId == groupInfo.NutritionistId)
        {
            await _mediator.Send(new AddConnectionToGroupCommand(orderId, new ConnectionDto
            {
                ConnectionId = connectionId,
                Username = _currentUserService.Username
            }));

            await Groups.AddToGroupAsync(Context.ConnectionId, orderId);

            await _mediator.Send(new ReadAllUnReadMessagesCommand(groupInfo.Id, _currentUserService.UserId, otherUserId));
            
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var orderId = Context.GetHttpContext().Request.Query["orderId"].ToString();
        var groupInfo = await _mediator.Send(new GetGroupByNameQuery(orderId));
        await _mediator.Send(new RemoveConnectionFromGroupCommand(groupInfo.Id, Context.ConnectionId));
        await base.OnDisconnectedAsync(exception);
    }


    public async Task SendMessage(CreateMessageDto dto)
    {
        var groupInfo = await _mediator.Send(new GetGroupByNameQuery(dto.GroupName));
        var currentUserId = _currentUserService.UserId == groupInfo.UserId ? groupInfo.UserId : groupInfo.NutritionistId;
        var otherUserId = _currentUserService.UserId == groupInfo.NutritionistId ? groupInfo.NutritionistId : groupInfo.UserId;
        var currentUserFullName = _currentUserService.FullName == groupInfo.UserFullName ? groupInfo.UserFullName : groupInfo.NutritionistFullName;
        var otherUserFullName = _currentUserService.FullName == groupInfo.NutritionistFullName ? groupInfo.NutritionistFullName : groupInfo.UserFullName;

        await _mediator.Send(new ReadAllUnReadMessagesCommand(groupInfo.Id, currentUserId, otherUserId));

        await _mediator.Send(new CreateMessageCommand(groupInfo.Id, currentUserId, currentUserFullName, otherUserId,
            otherUserFullName, dto.Content));

        var lang = _currentUserService.Language.ToEnum<Language>();
        switch (lang)
        {
            case Language.Persian:
                await PushNotification(dto, otherUserId, otherUserFullName, "پیام جدید از ");
                break;
            case Language.English:
                await PushNotification(dto, otherUserId, otherUserFullName, "New message from ");
                break;
            case Language.Arabic:
                await PushNotification(dto, otherUserId, otherUserFullName, "رسالة جديدة من ");
                break;
            default:
                await PushNotification(dto, otherUserId, otherUserFullName, "پیام جدید از ");
                break;
        }
        
        var result = new MessageResultDto(currentUserId, currentUserFullName, otherUserId, otherUserFullName,
            dto.Content, null, DateTime.UtcNow, MessageStatus.Send);

        await Clients.Groups(groupInfo.Name).SendAsync("NewMessage", result);
    }

    

    public async Task SendFile(CreateMessageDto dto)
    {
        var groupInfo = await _mediator.Send(new GetGroupByNameQuery(dto.GroupName));
        var currentUserId = _currentUserService.UserId == groupInfo.UserId ? groupInfo.UserId : groupInfo.NutritionistId;
        var otherUserId = _currentUserService.UserId == groupInfo.NutritionistId ? groupInfo.NutritionistId : groupInfo.UserId;
        var currentUserFullName = _currentUserService.FullName == groupInfo.UserFullName ? groupInfo.UserFullName : groupInfo.NutritionistFullName;
        var otherUserFullName = _currentUserService.FullName == groupInfo.NutritionistFullName ? groupInfo.NutritionistFullName : groupInfo.UserFullName;

        await _mediator.Send(new ReadAllUnReadMessagesCommand(groupInfo.Id , currentUserId, otherUserId));

        await _mediator.Send(new CreateMessageCommand(groupInfo.Id, currentUserId, currentUserFullName, otherUserId,
            otherUserFullName, dto.Content));

        var lang = _currentUserService.Language.ToEnum<Language>();
        switch (lang)
        {
            case Language.Persian:
                await PushNotification(dto, otherUserId, otherUserFullName, "پیام جدید از ");
                break;
            case Language.English:
                await PushNotification(dto, otherUserId, otherUserFullName, "New message from ");
                break;
            case Language.Arabic:
                await PushNotification(dto, otherUserId, otherUserFullName, "رسالة جديدة من ");
                break;
            default:
                await PushNotification(dto, otherUserId, otherUserFullName, "پیام جدید از ");
                break;
        }
        
        var result = new MessageResultDto(currentUserId, currentUserFullName, otherUserId, otherUserFullName,
            dto.Content, null, DateTime.UtcNow, MessageStatus.Send);

        await Clients.Groups(groupInfo.Name).SendAsync("NewFile", result);
    }
    
    private async Task PushNotification(CreateMessageDto dto, string otherUserId, string otherUserFullName, string text)
    {
        await _publishEndpoint.Publish<PushedNotification>(new
        {
            UserId = otherUserId,
            Title = text + otherUserFullName,
            Body = dto.Content.Length >= 10 ? dto.Content.Substring(0, 10)+" ..." : dto.Content
        });
    }
}