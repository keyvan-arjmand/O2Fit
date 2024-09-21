using Food.V2.Domain.Aggregates.FaultFoodAggregate;

namespace Food.V2.Application.ProblemFoods.V1.Commands.SoftDeleteMissingFood;

public class SoftDeleteProblemFoodCommandHandler : IRequestHandler<SoftDeleteProblemFoodCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteProblemFoodCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteProblemFoodCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<FaultFood>()
            .GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (result == null) throw new NotFoundException($"report Not Found");
        result.IsDelete = true;
        await _work.GenericRepository<FaultFood>()
            .SoftDeleteByIdAsync(request.Id, result, cancellationToken: cancellationToken);
    }
}