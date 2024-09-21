namespace Chat.Application.Groups.V1.Queries.GetGroupById;

public record GetGroupByIdQuery(string Id) : IRequest<GetGroupByIdDto>;