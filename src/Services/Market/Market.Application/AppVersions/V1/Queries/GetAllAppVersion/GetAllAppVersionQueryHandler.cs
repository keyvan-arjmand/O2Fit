using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.AppVersionAggregate;

namespace Market.Application.AppVersions.V1.Queries.GetAllAppVersion;

public class GetAllAppVersionQueryHandler : IRequestHandler<GetAllAppVersionQuery, PaginationResult<AppVersionDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllAppVersionQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<PaginationResult<AppVersionDto>> Handle(GetAllAppVersionQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<AppVersion>()
            .GetAllPaginationAsync(request.Page, request.PageSize, cancellationToken);
        return PaginationResult<AppVersionDto>.CreatePaginationResult(request.Page, request.PageSize, result.Data.Count,
            result.Data.ToDto<AppVersionDto>().ToList());
    }
}