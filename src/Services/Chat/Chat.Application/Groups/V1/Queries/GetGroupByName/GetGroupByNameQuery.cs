namespace Chat.Application.Groups.V1.Queries.GetGroupByName;

public record GetGroupByNameQuery(string Name) : IRequest<GroupDto>;