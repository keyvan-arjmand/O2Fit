namespace Chat.Application.Groups.V1.Commands.DeleteGroup;

public record DeleteGroupCommand(string Id) : IRequest;