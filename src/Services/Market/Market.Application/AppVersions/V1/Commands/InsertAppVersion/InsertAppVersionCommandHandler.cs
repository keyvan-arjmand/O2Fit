using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Interfaces.Services;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.AppVersionAggregate;

namespace Market.Application.AppVersions.V1.Commands.InsertAppVersion;

public class InsertAppVersionCommandHandler : IRequestHandler<InsertAppVersionCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IResponseCacheService _cacheService;

    public InsertAppVersionCommandHandler(IUnitOfWork work, IResponseCacheService cacheService)
    {
        _work = work;
        _cacheService = cacheService;
    }

    public async Task Handle(InsertAppVersionCommand request, CancellationToken cancellationToken)
    {
        var appVersion = new AppVersion
        {
            MarketTypes = request.MarketTypes,
            Version = request.Version,
            Link = request.Link,
            Description = request.Description.MapTo<TranslationAppVersion, TranslationDto>(),
            IsForced = request.IsForced
        };
        await _work.GenericRepository<AppVersion>().InsertOneAsync(appVersion, null, cancellationToken);
        // await _cacheService.CacheResponseAsync(appVersion., appVersion, TimeSpan.FromMinutes(5));
    }
}