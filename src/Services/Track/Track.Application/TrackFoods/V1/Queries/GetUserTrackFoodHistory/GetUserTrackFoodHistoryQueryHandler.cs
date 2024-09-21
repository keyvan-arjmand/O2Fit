using MongoDB.Bson;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackFoodAggregate;

namespace Track.Application.TrackFoods.V1.Queries.GetUserTrackFoodHistory;

public class GetUserTrackFoodHistoryQueryHandler : IRequestHandler<GetUserTrackFoodHistoryQuery, List<UserTrackFoodDto>>
{
    private readonly IUnitOfWork _work;

    public GetUserTrackFoodHistoryQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<UserTrackFoodDto>> Handle(GetUserTrackFoodHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<TrackFood>.Filter.Where(x =>
            x.CreatedById == request.UserId.StringToInt() && x.InsertDate >= request.DateTime);
        var userTrack = await _work.GenericRepository<TrackFood>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return userTrack.ToDto<UserTrackFoodDto>().ToList();
    }
}