namespace Workout.V2.Application.Workouts.V1.Queries.GetBodyBuildingPaginated;

public class GetBodyBuildingPaginatedQueryHandler : IRequestHandler<GetBodyBuildingPaginatedQuery, PaginationResult<BodyBuildingDto>>
{
    private readonly IUnitOfWork _uow;

    public GetBodyBuildingPaginatedQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PaginationResult<BodyBuildingDto>> Handle(GetBodyBuildingPaginatedQuery request, CancellationToken cancellationToken)
    {
        var filter =
            Builders<Domain.Aggregates.WorkoutsAggregate.Workout>.Filter.Eq(x => x.Classification,
                Classification.BodyBuilding);
        filter &= Builders<Domain.Aggregates.WorkoutsAggregate.Workout>.Filter.Eq(x => x.IsDelete, false);

        var workouts = await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .GetListPaginationOfDocumentsByFilterAsync(request.Page, request.PageSize, filter, cancellationToken);

        var bodyBuildings = new List<BodyBuildingDto>();
        
        foreach (var workout in workouts.Data)
        {
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
            
            bodyBuildings.Add(result);
        }
        
        return PaginationResult<BodyBuildingDto>.CreatePaginationResult(request.Page, request.PageSize, workouts.Count, bodyBuildings);
        
    }
}