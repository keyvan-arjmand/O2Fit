using MongoDB.Bson;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackFoodAggregate;

namespace Track.Application.TrackFoods.V1.Queries.GetUserTrackByDate;

public class GetUserTrackByDateCommandHandler : IRequestHandler<GetUserTrackByDateCommand, List<UserTrackFoodDto>>
{
    private readonly IUnitOfWork _work;

    public GetUserTrackByDateCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<UserTrackFoodDto>> Handle(GetUserTrackByDateCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<TrackFood>.Filter.Eq(x => x.CreatedById, request.UserId.StringToInt());
        filter &= Builders<TrackFood>.Filter.Eq(x => x.InsertDate, request.Date);
        var userTrack = await _work.GenericRepository<TrackFood>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return userTrack.ToDto<UserTrackFoodDto>().ToList();
    }
}