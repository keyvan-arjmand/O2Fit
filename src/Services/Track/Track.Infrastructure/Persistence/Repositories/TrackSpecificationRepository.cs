using MediatR;
using MongoDB.Driver;
using Track.Application.Common.Interfaces.Persistence.Repositories;
using Track.Domain.Aggregates.TrackSpecificationAggregate;

namespace Track.Infrastructure.Persistence.Repositories;

public class TrackSpecificationRepository : GenericRepository<TrackSpecification>, ITrackSpecificationRepository
{
    public TrackSpecificationRepository(IMediator mediator, IConfiguration configuration,
        ICurrentUserService currentUserService) : base(mediator, configuration, currentUserService)
    {
    }

    public async Task<List<TrackSpecification>> GetLastTrackSpecification(FilterDefinition<TrackSpecification> filter,
        CancellationToken cancellationToken = default)
    {
        var documents = await Collection.Find(filter).SortByDescending(x => x.InsertDate).Limit(2)
            .ToListAsync(cancellationToken);
        return documents;
    }

    public async Task<TrackSpecification> GetSingleLastTrackUserSpecification(
        FilterDefinition<TrackSpecification> filter, CancellationToken cancellationToken = default)
    {
        var documents = await Collection.Find(filter).SortByDescending(x => x.InsertDate).Limit(1)
            .SingleOrDefaultAsync(cancellationToken);
        return documents;
    }

    public async Task<List<TrackSpecification>> GetPaginationTrackUserSpecification(int pageSize,
        FilterDefinition<TrackSpecification> filter, CancellationToken cancellationToken = default)
    {
        var documents = await Collection.Find(filter).SortByDescending(x => x.InsertDate).Limit(pageSize)
            .ToListAsync(cancellationToken);
        return documents!;
    }
}