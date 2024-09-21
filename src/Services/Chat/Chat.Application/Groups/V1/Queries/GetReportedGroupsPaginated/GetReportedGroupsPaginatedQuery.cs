namespace Chat.Application.Groups.V1.Queries.GetReportedGroupsPaginated;

public record GetReportedGroupsPaginatedQuery(int Page, int PageSize) : IRequest<PaginationResult<GetGroupPaginatedDto>>;