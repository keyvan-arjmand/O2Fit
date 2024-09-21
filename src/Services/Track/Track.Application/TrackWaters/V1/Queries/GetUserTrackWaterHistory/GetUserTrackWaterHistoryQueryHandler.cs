using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackWaterAggregate;

namespace Track.Application.TrackWaters.V1.Queries.GetUserTrackWaterHistory;

public class
    GetUserTrackWaterHistoryQueryHandler : IRequestHandler<GetUserTrackWaterHistoryQuery, List<UserTrackWaterDto>>
{
    private readonly IUnitOfWork _work;

    public GetUserTrackWaterHistoryQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<UserTrackWaterDto>> Handle(GetUserTrackWaterHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<TrackWater>.Filter.Where(x =>
            x.InsertDate >= request.DateTime && x.CreatedById ==request.UserId.StringToInt());
        var result = await _work.GenericRepository<TrackWater>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return result.ToDto<UserTrackWaterDto>().ToList();
    }
}