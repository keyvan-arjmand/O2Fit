using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Domain.Aggregates.FavoriteFoodAggregate;

namespace Track.Application.FavoriteFoods.V1.Commands.SoftDeleteFavoriteFood;

public class SoftDeleteFavoriteFoodCommandHandler : IRequestHandler<SoftDeleteFavoriteFoodCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteFavoriteFoodCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteFavoriteFoodCommand request, CancellationToken cancellationToken)
    {
        var food = await _work.GenericRepository<FavoriteFood>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (food == null)
            throw new NotFoundException(nameof(FavoriteFood), request.Id);
        await _work.GenericRepository<FavoriteFood>()
            .SoftDeleteByIdAsync(request.Id, food, null, cancellationToken);
    }
}