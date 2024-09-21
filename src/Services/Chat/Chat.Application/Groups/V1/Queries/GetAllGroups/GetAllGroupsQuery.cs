namespace Chat.Application.Groups.V1.Queries.GetAllGroups;

public record GetAllGroupsQuery() : IRequest<List<GroupDto>>;