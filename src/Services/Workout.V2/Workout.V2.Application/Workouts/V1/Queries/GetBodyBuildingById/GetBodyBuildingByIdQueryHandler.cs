namespace Workout.V2.Application.Workouts.V1.Queries.GetBodyBuildingById;

public class GetBodyBuildingByIdQueryHandler : IRequestHandler<GetBodyBuildingByIdQuery , BodyBuildingDto>
{
    private readonly IUnitOfWork _uow;

    public GetBodyBuildingByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<BodyBuildingDto> Handle(GetBodyBuildingByIdQuery request, CancellationToken cancellationToken)
    {
        var workout = await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (workout == null)
            throw new NotFoundException(nameof(Domain.Aggregates.WorkoutsAggregate.Workout), request.Id);

        var bodyMuscleTranslations = new List<BodyMuscleTranslationDto>();
        
        foreach (var id in workout.BodyMuscleIds)
        {
            var bodyMuscle = await _uow.GenericRepository<BodyMuscle>().GetByIdAsync(id.ToString(), cancellationToken);
            if (bodyMuscle == null)
                throw new NotFoundException(nameof(BodyMuscle), id.ToString());

            var bodyMuscleTranslation = new BodyMuscleTranslationDto(bodyMuscle.Name.Persian, bodyMuscle.Name.English,
                bodyMuscle.Name.Arabic);
            bodyMuscleTranslations.Add(bodyMuscleTranslation);
        }

        var result = workout.ToDto<BodyBuildingDto>();
        result.BodyMuscles.AddRange(bodyMuscleTranslations);

        return result;
    }
}