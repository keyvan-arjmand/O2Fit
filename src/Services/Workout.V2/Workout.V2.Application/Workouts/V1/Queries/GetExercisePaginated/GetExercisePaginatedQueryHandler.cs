namespace Workout.V2.Application.Workouts.V1.Queries.GetExercisePaginated;

public class GetExercisePaginatedQueryHandler : IRequestHandler<GetExercisePaginatedQuery , PaginationResult<ExerciseDto>>
{
    private readonly IUnitOfWork _uow;

    public GetExercisePaginatedQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PaginationResult<ExerciseDto>> Handle(GetExercisePaginatedQuery request, CancellationToken cancellationToken)
    {
        var filter =
            Builders<Domain.Aggregates.WorkoutsAggregate.Workout>.Filter.Eq(x => x.Classification,
                Classification.Exercise);

        filter &= Builders<Domain.Aggregates.WorkoutsAggregate.Workout>.Filter.Eq(x => x.IsDelete, false);
            
        var paginatedData = await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .GetListPaginationOfDocumentsByFilterAsync(request.Page, request.PageSize, filter, cancellationToken);

        var data = paginatedData.Data.ToDto<ExerciseDto>().ToList();
        return PaginationResult<ExerciseDto>.CreatePaginationResult(request.Page, request.PageSize, data.Count, data);
    }
}