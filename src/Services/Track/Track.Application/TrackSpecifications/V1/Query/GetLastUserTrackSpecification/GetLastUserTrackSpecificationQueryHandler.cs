using EventBus.Messages.Contracts.Services.Track.Specification;
using MassTransit;
using MongoDB.Bson;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackSpecificationAggregate;

namespace Track.Application.TrackSpecifications.V1.Query.GetLastUserTrackSpecification;

public class
    GetLastUserTrackSpecificationQueryHandler : IRequestHandler<GetLastUserTrackSpecificationQuery,
        List<UserTrackSpecificationDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<UserTrackSpecificationRequest> _client;
    public GetLastUserTrackSpecificationQueryHandler(IUnitOfWork uow, IRequestClient<UserTrackSpecificationRequest> client)
    {
        _uow = uow;
        _client = client;
    }

    public async Task<List<UserTrackSpecificationDto>> Handle(GetLastUserTrackSpecificationQuery request,
        CancellationToken cancellationToken)
    {
        var userId = request.Id.StringToInt();
        var filter = Builders<TrackSpecification>.Filter.Eq(x => x.CreatedById, userId);
        var userTracks = await _uow.TrackSpecificationRepository()
            .GetLastTrackSpecification(filter, cancellationToken);
        return userTracks.ToDto<UserTrackSpecificationDto>().ToList();
    }
}