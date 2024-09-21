using Food.V2.Domain.Aggregates.MissingFoodAggregate;

namespace Food.V2.Application.MissingFoods.V1.Commands.SoftDeleteMissingFood;

public class SoftDeleteMissingFoodCommandHandler : IRequestHandler<SoftDeleteMissingFoodCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteMissingFoodCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteMissingFoodCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<MissingFood>()
            .GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (result == null) throw new NotFoundException($"report Not Found");
        result.IsDelete = true;
        await _work.GenericRepository<MissingFood>()
            .SoftDeleteByIdAsync(request.Id, result, cancellationToken: cancellationToken);
    }
}