using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.AppVersionAggregate;

namespace Market.Application.AppVersions.V1.Commands.UpdateAppVersion;

public class UpdateAppVersionCommandHandler : IRequestHandler<UpdateAppVersionCommand>
{
    private readonly IUnitOfWork _work;

    public UpdateAppVersionCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(UpdateAppVersionCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<AppVersion>().GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException($"AppVersion not found {request.Id}");
        result.IsForced = request.IsForced;
        result.Description.Persian = request.Description.Persian;
        result.Description.English = request.Description.English;
        result.Description.Arabic = request.Description.Arabic;
        result.Link = request.Link;
        result.Version = request.Version;
        result.MarketTypes = request.MarketTypes;
        await _work.GenericRepository<AppVersion>()
            .UpdateOneAsync(x => x.Id == request.Id, result,
                new Expression<Func<AppVersion, object>>[]
                {
                    x => x.IsForced,
                    x => x.Description.Persian,
                    x => x.Description.Arabic,
                    x => x.Description.English,
                    x => x.Link,
                    x => x.Version,
                    x => x.MarketTypes,
                }, null, cancellationToken);
        // await _cacheService.CacheResponseAsync(appVersion., appVersion, TimeSpan.FromMinutes(5));
    }
}