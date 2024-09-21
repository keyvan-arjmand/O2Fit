using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.AppVersionAggregate;

namespace Market.Application.AppVersions.V1.Queries.GetAppVersion;

public class GetAppVersionQueryHandler:IRequestHandler<GetAppVersionQuery,AppVersionDto>
{
    private readonly IUnitOfWork _work;

    public GetAppVersionQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<AppVersionDto> Handle(GetAppVersionQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<AppVersion>.Filter.Where(x =>
            x.Version.Contains(request.AppVersion) && x.MarketTypes.Contains(request.MarketType));
        var result = await _work.GenericRepository<AppVersion>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        return result.ToDto<AppVersionDto>();
    }
}