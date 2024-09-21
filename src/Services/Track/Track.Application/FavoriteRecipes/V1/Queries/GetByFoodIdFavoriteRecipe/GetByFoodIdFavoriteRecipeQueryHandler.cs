using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.FavoriteRecipeAggregate;

namespace Track.Application.FavoriteRecipes.V1.Queries.GetByFoodIdFavoriteRecipe;

public class GetByFoodIdFavoriteRecipeQueryHandler : IRequestHandler<GetByFoodIdFavoriteRecipeQuery, TrackRecipeDto>
{
    private readonly IUnitOfWork _work;

    public GetByFoodIdFavoriteRecipeQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<TrackRecipeDto> Handle(GetByFoodIdFavoriteRecipeQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<FavoriteRecipe>.Filter.Eq(x => x.FoodId, ObjectId.Parse(request.FoodId));
         filter &= Builders<FavoriteRecipe>.Filter.Eq(x => x.UserId, request.UserId.StringToInt());
        var track = await _work.GenericRepository<FavoriteRecipe>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (track == null) throw new NotFoundException("Track not found");
        return track.ToDto<TrackRecipeDto>();
    }
}