namespace Chat.Application.Groups.V1.Commands.AddConnectionToGroup;

public record AddConnectionToGroupCommand(string GroupName, ConnectionDto Connection) : IRequest;
