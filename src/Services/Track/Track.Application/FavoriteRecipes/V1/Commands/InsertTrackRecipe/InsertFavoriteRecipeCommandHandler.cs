using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Dtos;
using Track.Domain.Aggregates.FavoriteRecipeAggregate;

namespace Track.Application.FavoriteRecipes.V1.Commands.InsertTrackRecipe;

public class InsertFavoriteRecipeCommandHandler : IRequestHandler<InsertFavoriteRecipeCommand, TrackRecipeDto>
{
    private readonly IUnitOfWork _work;

    public InsertFavoriteRecipeCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<TrackRecipeDto> Handle(InsertFavoriteRecipeCommand request, CancellationToken cancellationToken)
    {
        if (await _work.GenericRepository<FavoriteRecipe>()
                .AnyAsync(x => x.FoodId == ObjectId.Parse(request.FoodId), cancellationToken))
            throw new AppException("Track recipe already exist");
        await _work.GenericRepository<FavoriteRecipe>().InsertOneAsync(new FavoriteRecipe
        {
            UserId = request.UserId.StringToInt(),
            FoodId = ObjectId.Parse(request.FoodId),
        }, null, cancellationToken);
        return new TrackRecipeDto
        {
            FoodId = request.FoodId,
            UserId = request.UserId
        };
    }
}