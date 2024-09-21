using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Domain.Aggregates.FavoriteMealAggregate;

namespace Track.Application.FavoriteMeals.V1.Commands.SoftDeleteFavoriteMeal;

public class SoftDeleteFavoriteMealCommandHandler:IRequestHandler<SoftDeleteFavoriteMealCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteFavoriteMealCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteFavoriteMealCommand request, CancellationToken cancellationToken)
    {
        var track = await _work.GenericRepository<FavoriteMeal>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (track == null) throw new NotFoundException("track Not Found");
        track.IsDelete = true;
        await _work.GenericRepository<FavoriteMeal>()
            .SoftDeleteByIdAsync(request.Id, track, null, cancellationToken).ConfigureAwait(false);

    }
}