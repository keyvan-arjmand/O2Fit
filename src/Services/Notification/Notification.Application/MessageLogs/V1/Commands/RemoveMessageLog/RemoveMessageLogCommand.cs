namespace Notification.Application.MessageLogs.V1.Commands.RemoveMessageLog;

public record RemoveMessageLogCommand(string Id): IRequest;