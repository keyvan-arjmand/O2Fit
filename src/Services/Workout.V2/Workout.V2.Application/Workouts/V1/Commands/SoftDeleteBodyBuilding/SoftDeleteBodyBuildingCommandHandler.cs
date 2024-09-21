namespace Workout.V2.Application.Workouts.V1.Commands.SoftDeleteBodyBuilding;

public class SoftDeleteBodyBuildingCommandHandler : IRequestHandler<SoftDeleteBodyBuildingCommand>
{
    private readonly IUnitOfWork _uow;

    public SoftDeleteBodyBuildingCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(SoftDeleteBodyBuildingCommand request, CancellationToken cancellationToken)
    {
        var workout = await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (workout == null)
            throw new NotFoundException(nameof(Domain.Aggregates.WorkoutsAggregate.Workout), request.Id);

        await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .SoftDeleteByIdAsync(request.Id, workout, null, cancellationToken);
    }
}