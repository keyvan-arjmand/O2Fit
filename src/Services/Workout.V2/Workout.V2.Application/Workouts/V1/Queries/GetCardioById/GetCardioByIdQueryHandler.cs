namespace Workout.V2.Application.Workouts.V1.Queries.GetCardioById;

public class GetCardioByIdQueryHandler : IRequestHandler<GetCardioByIdQuery, CardioDto>
{
    private readonly IUnitOfWork _uow;

    public GetCardioByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<CardioDto> Handle(GetCardioByIdQuery request, CancellationToken cancellationToken)
    {
        var workout = await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (workout == null)
            throw new NotFoundException(nameof(Domain.Aggregates.WorkoutsAggregate.Workout), request.Id);

        return workout.ToDto<CardioDto>();
    }
}