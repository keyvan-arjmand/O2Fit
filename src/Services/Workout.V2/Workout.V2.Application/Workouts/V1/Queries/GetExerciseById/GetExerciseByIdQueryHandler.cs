namespace Workout.V2.Application.Workouts.V1.Queries.GetExerciseById;

public class GetExerciseByIdQueryHandler : IRequestHandler<GetExerciseByIdQuery, ExerciseDto>
{
    private readonly IUnitOfWork _uow;

    public GetExerciseByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ExerciseDto> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
    {
        var workout = await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .GetByIdAsync(request.Id, cancellationToken);
        
        if (workout == null)
            throw new NotFoundException(nameof(Domain.Aggregates.WorkoutsAggregate.Workout), request.Id);

        return workout.ToDto<ExerciseDto>();
    }
}