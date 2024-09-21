using Amazon.Runtime.Internal.Transform;
using MongoDB.Bson;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackFoodAggregate;
using Track.Domain.Aggregates.TrackNutrientAggregate;

namespace Track.Application.TrackFoods.V1.Queries.GetUserTrackByMeal;

public class GetUserTrackByMealCommandHandler : IRequestHandler<GetUserTrackByMealCommand, List<UserTrackFoodDto>>
{
    private readonly IUnitOfWork _work;

    public GetUserTrackByMealCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<UserTrackFoodDto>> Handle(GetUserTrackByMealCommand request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<TrackFood>.Filter.Eq(x => x.CreatedById, request.UserId.StringToInt());
        filter &= Builders<TrackFood>.Filter.Eq(x => x.InsertDate, request.Date);
        filter &= Builders<TrackFood>.Filter.Eq(x => x.FoodMeal, request.FoodMeal);
        var userTrack = await _work.GenericRepository<TrackFood>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return userTrack.ToDto<UserTrackFoodDto>().ToList();
    }
}