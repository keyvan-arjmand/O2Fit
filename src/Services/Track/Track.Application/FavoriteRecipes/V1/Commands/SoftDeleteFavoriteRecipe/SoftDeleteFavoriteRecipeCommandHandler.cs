using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Domain.Aggregates.FavoriteRecipeAggregate;

namespace Track.Application.FavoriteRecipes.V1.Commands.SoftDeleteFavoriteRecipe;

public class SoftDeleteFavoriteRecipeCommandHandler : IRequestHandler<SoftDeleteFavoriteRecipeCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteFavoriteRecipeCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteFavoriteRecipeCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<FavoriteRecipe>.Filter.Eq(x => x.FoodId, ObjectId.Parse(request.FoodId));
        var track = await _work.GenericRepository<FavoriteRecipe>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (track == null) throw new NotFoundException("Track not found");
        track.IsDelete = true;
        await _work.GenericRepository<FavoriteRecipe>().SoftDeleteByIdAsync(track.Id, track, null, cancellationToken);
    }
}