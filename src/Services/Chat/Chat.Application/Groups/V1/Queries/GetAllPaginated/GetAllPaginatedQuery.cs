namespace Chat.Application.Groups.V1.Queries.GetAllPaginated;

public record GetAllPaginatedQuery(int Page, int PageSize) : IRequest<PaginationResult<GetGroupPaginatedDto>>;