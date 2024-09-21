namespace Food.V2.Application.Foods.V1.Commands.SoftDeleteFood;

public class SoftDeleteFoodCommandHandler : IRequestHandler<SoftDeleteFoodCommand>
{
    private readonly IUnitOfWork _uow;

    public SoftDeleteFoodCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(SoftDeleteFoodCommand request, CancellationToken cancellationToken)
    {
        var food = await _uow.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (food == null)
            throw new NotFoundException(nameof(Domain.Aggregates.FoodAggregate.Food), request.Id);

        await _uow.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .SoftDeleteByIdAsync(request.Id, food, null, cancellationToken);
    }
}