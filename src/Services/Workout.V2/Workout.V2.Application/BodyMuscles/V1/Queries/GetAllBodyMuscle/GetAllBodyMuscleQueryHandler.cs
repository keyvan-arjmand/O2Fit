namespace Workout.V2.Application.BodyMuscles.V1.Queries.GetAllBodyMuscle;

public class GetAllBodyMuscleQueryHandler : IRequestHandler<GetAllBodyMuscleQuery, List<BodyMuscleDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllBodyMuscleQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<BodyMuscleDto>> Handle(GetAllBodyMuscleQuery request, CancellationToken cancellationToken)
    {
        var allBodyMuscles = await _uow.GenericRepository<BodyMuscle>().GetAllAsync(cancellationToken);
        return allBodyMuscles.ToDto<BodyMuscleDto>().ToList();
    }
}