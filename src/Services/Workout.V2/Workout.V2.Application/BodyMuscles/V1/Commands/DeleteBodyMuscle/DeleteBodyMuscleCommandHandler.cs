namespace Workout.V2.Application.BodyMuscles.V1.Commands.DeleteBodyMuscle;

public class DeleteBodyMuscleCommandHandler : IRequestHandler<DeleteBodyMuscleCommand>
{
    private readonly IUnitOfWork _uow;

    public DeleteBodyMuscleCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(DeleteBodyMuscleCommand request, CancellationToken cancellationToken)
    {
        var bodyMuscle = await _uow.GenericRepository<BodyMuscle>().GetByIdAsync(request.Id, cancellationToken);
        
        if (bodyMuscle == null)
            throw new NotFoundException(nameof(BodyMuscle), request.Id);

        await _uow.GenericRepository<BodyMuscle>()
            .DeleteOneAsync(x => x.Id == request.Id, bodyMuscle, null, cancellationToken);
    }
}