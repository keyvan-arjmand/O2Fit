using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Domain.Aggregates.FavoriteMealAggregate;

namespace Track.Application.FavoriteMeals.V1.Commands.DeleteFavoriteMealFood;

public class DeleteFavoriteMealFoodCommandHandler : IRequestHandler<DeleteFavoriteMealFoodCommand>
{
    private readonly IUnitOfWork _work;

    public DeleteFavoriteMealFoodCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(DeleteFavoriteMealFoodCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<FavoriteMeal>.Filter.Eq(x => x.UserId, request.UserId.StringToInt());
        filter &= Builders<FavoriteMeal>.Filter.Eq(x => x.Meal, request.Meal);
        var track = await _work.GenericRepository<FavoriteMeal>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (track == null) throw new NotFoundException($"track Not Found {request.UserId}");
        track.Foods = track.Foods.Where(x => x.Id != request.FoodId).ToList();
        await _work.GenericRepository<FavoriteMeal>()
            .UpdateOneAsync(x => x.Id == track.Id, track,
                new Expression<Func<FavoriteMeal, object>>[]
                {
                    x => x.Foods,
                }, null, cancellationToken);
    }
}