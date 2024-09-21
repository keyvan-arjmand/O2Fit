namespace Chat.Application.Groups.V1.Commands.CreateGroupWithConnection;

public record CreateGroupWithConnectionCommand(string GroupName, List<ConnectionDto> Connections): IRequest;