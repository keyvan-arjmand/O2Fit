namespace Workout.V2.Application.Workouts.V1.Queries.GetCardioPaginated;

public class GetCardioPaginatedQueryHandler : IRequestHandler<GetCardioPaginatedQuery, PaginationResult<CardioDto>>
{
    private readonly IUnitOfWork _uow;

    public GetCardioPaginatedQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PaginationResult<CardioDto>> Handle(GetCardioPaginatedQuery request, CancellationToken cancellationToken)
    {
        var filter =
            Builders<Domain.Aggregates.WorkoutsAggregate.Workout>.Filter.Eq(x => x.Classification,
                Classification.Cardio);

        filter &= Builders<Domain.Aggregates.WorkoutsAggregate.Workout>.Filter.Eq(x => x.IsDelete, false);
        
        var paginateData = await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .GetListPaginationOfDocumentsByFilterAsync(request.Page, request.PageSize, filter, cancellationToken);

        return PaginationResult<CardioDto>.CreatePaginationResult(request.Page, request.PageSize, paginateData.Count,
            paginateData.Data.ToDto<CardioDto>().ToList());
    }
}