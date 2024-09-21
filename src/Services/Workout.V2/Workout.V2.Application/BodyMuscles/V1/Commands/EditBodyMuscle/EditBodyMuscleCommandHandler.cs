namespace Workout.V2.Application.BodyMuscles.V1.Commands.EditBodyMuscle;

public class EditBodyMuscleCommandHandler : IRequestHandler<EditBodyMuscleCommand>
{
    private readonly IUnitOfWork _uow;

    public EditBodyMuscleCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(EditBodyMuscleCommand request, CancellationToken cancellationToken)
    {
        var bodyMuscle = await _uow.GenericRepository<BodyMuscle>().GetByIdAsync(request.Id, cancellationToken);
        if (bodyMuscle == null)
            throw new NotFoundException(nameof(BodyMuscle), request.Id);

        bodyMuscle.Name.Arabic = request.Arabic;
        bodyMuscle.Name.English = request.English;
        bodyMuscle.Name.Persian = request.Persian;

        await _uow.GenericRepository<BodyMuscle>().UpdateOneAsync(x => x.Id == request.Id, bodyMuscle,
            new Expression<Func<BodyMuscle, object>>[]
            {
                x=>x.Name
               // x => x.Name.Arabic,
               // x => x.Name.English,
               // x => x.Name.Persian
            }, null, cancellationToken);

    }
}