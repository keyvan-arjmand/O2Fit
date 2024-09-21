namespace Workout.V2.Application.BodyMuscles.V1.Commands.SoftDeleteBodyMuscle;

public class SoftDeleteBodyMuscleCommandHandler : IRequestHandler<SoftDeleteBodyMuscleCommand>
{
    private readonly IUnitOfWork _uow;

    public SoftDeleteBodyMuscleCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(SoftDeleteBodyMuscleCommand request, CancellationToken cancellationToken)
    {
        var bodyMuscle = await _uow.GenericRepository<BodyMuscle>().GetByIdAsync(request.Id, cancellationToken);
        if (bodyMuscle == null)
            throw new NotFoundException(nameof(BodyMuscle), request.Id);

        await _uow.GenericRepository<BodyMuscle>().SoftDeleteByIdAsync(request.Id, bodyMuscle, null, cancellationToken);
    }
}