using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.FavoriteMealAggregate;

namespace Track.Application.FavoriteMeals.V1.Queries.GetByMealFavoriteMeal;

public class GetByMealFavoriteMealQueryHandler : IRequestHandler<GetByMealFavoriteMealQuery, List<TrackMealDto>>
{
    private readonly IUnitOfWork _work;

    public GetByMealFavoriteMealQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<TrackMealDto>> Handle(GetByMealFavoriteMealQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<FavoriteMeal>.Filter.Eq(x => x.UserId, request.UserId.StringToInt());
        filter &= Builders<FavoriteMeal>.Filter.Eq(x => x.Meal, request.Meal);
        var tracks = await _work.GenericRepository<FavoriteMeal>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        if (tracks == null) throw new NotFoundException("track not found");
        return tracks.ToDto<TrackMealDto>().ToList();
    }
}