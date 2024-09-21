namespace Workout.V2.Application.BodyMuscles.V1.Commands.CreateBodyMuscle;

public class CreateBodyMuscleCommandHandler : IRequestHandler<CreateBodyMuscleCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateBodyMuscleCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(CreateBodyMuscleCommand request, CancellationToken cancellationToken)
    {
        var bodyMuscle = new BodyMuscle(request.Translation.ToEntity<BodyMuscleTranslation>());
        await _uow.GenericRepository<BodyMuscle>().InsertOneAsync(bodyMuscle, null, cancellationToken);
    }
}