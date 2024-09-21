using MongoDB.Bson;
using Track.Domain.Aggregates.TrackSpecificationAggregate;

namespace Track.Application.Common.Interfaces.Persistence.Repositories;

public interface ITrackSpecificationRepository : IGenericRepository<TrackSpecification>
{
    public Task<List<TrackSpecification>> GetLastTrackSpecification(FilterDefinition<TrackSpecification> filter,
        CancellationToken cancellationToken = default);

    public Task<TrackSpecification> GetSingleLastTrackUserSpecification(FilterDefinition<TrackSpecification> filter,
        CancellationToken cancellationToken = default);

    public Task<List<TrackSpecification>> GetPaginationTrackUserSpecification(int pageSize,
        FilterDefinition<TrackSpecification> filter, CancellationToken cancellationToken = default);
}