namespace Workout.V2.Application.BodyMuscles.V1.Queries.GetBodyMuscleById;

public class GetBodyMuscleByIdQueryHandler : IRequestHandler<GetBodyMuscleByIdQuery , BodyMuscleDto>
{
    private readonly IUnitOfWork _uow;

    public GetBodyMuscleByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<BodyMuscleDto> Handle(GetBodyMuscleByIdQuery request, CancellationToken cancellationToken)
    {
        var bodyMuscle = await _uow.GenericRepository<BodyMuscle>().GetByIdAsync(request.Id, cancellationToken);

        if (bodyMuscle == null)
            throw new NotFoundException(nameof(BodyMuscle), request.Id);

        return bodyMuscle.ToDto<BodyMuscleDto>();
    }
}