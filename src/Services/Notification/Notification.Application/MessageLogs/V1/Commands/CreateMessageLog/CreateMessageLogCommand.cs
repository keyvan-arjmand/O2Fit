namespace Notification.Application.MessageLogs.V1.Commands.CreateMessageLog;

public record CreateMessageLogCommand(string Text, string ToFcmToken, string ToPhoneNumber) : IRequest;