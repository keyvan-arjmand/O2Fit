using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.FavoriteMealAggregate;

namespace Track.Application.FavoriteMeals.V1.Queries.GetAllFavoriteMeals;

public class GetAllTrackMealQueryHandler : IRequestHandler<GetAllTrackMealQuery, List<TrackMealDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllTrackMealQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<TrackMealDto>> Handle(GetAllTrackMealQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<FavoriteMeal>.Filter.Eq(x => x.UserId, request.UserId.StringToInt());
        var tracks = await _work.GenericRepository<FavoriteMeal>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        if (tracks == null) throw new NotFoundException("track not found");
        return tracks.OrderByDescending(x=>x.Created).ToDto<TrackMealDto>().ToList();
    }
}