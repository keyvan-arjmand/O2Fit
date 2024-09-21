namespace Chat.Application.Groups.V1.Commands.RemoveConnectionFromGroup;

public record RemoveConnectionFromGroupCommand(string GroupId, string ConnectionId) : IRequest;