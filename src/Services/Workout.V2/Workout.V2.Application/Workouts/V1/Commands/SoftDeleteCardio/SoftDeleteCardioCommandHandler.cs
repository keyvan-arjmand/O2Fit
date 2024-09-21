namespace Workout.V2.Application.Workouts.V1.Commands.SoftDeleteCardio;

public class SoftDeleteCardioCommandHandler : IRequestHandler<SoftDeleteCardioCommand>
{
    private readonly IUnitOfWork _uow;

    public SoftDeleteCardioCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(SoftDeleteCardioCommand request, CancellationToken cancellationToken)
    {
        var workout = await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (workout == null)
            throw new NotFoundException(nameof(Domain.Aggregates.WorkoutsAggregate.Workout), request.Id);

        await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .SoftDeleteByIdAsync(request.Id, workout, null, cancellationToken);
    }
}