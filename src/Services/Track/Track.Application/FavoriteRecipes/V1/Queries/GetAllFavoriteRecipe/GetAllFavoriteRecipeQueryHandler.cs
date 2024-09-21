using MongoDB.Bson;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.FavoriteRecipeAggregate;

namespace Track.Application.FavoriteRecipes.V1.Queries.GetAllFavoriteRecipe;

public class GetAllFavoriteRecipeQueryHandler : IRequestHandler<GetAllFavoriteRecipeQuery, List<TrackRecipeDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllFavoriteRecipeQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<TrackRecipeDto>> Handle(GetAllFavoriteRecipeQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<FavoriteRecipe>.Filter.Eq(x => x.UserId,request.UserId.StringToInt());
        var tracks = await _work.GenericRepository<FavoriteRecipe>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return tracks.OrderByDescending(x=>x.Created).ToDto<TrackRecipeDto>().ToList();
    }
}