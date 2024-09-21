using Market.Application.Dtos;

namespace Market.Application.AppVersions.V1.Queries.GetAllAppVersion;

public record GetAllAppVersionQuery(int Page, int PageSize) : IRequest<PaginationResult<AppVersionDto>>;