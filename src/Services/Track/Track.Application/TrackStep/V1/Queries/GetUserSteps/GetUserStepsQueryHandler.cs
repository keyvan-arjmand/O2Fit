using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;

namespace Track.Application.TrackStep.V1.Queries.GetUserSteps;

public class GetUserStepsQueryHandler : IRequestHandler<GetUserStepsQuery, List<UserStepsDto>>
{
    private readonly IUnitOfWork _work;

    public GetUserStepsQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<UserStepsDto>> Handle(GetUserStepsQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.TrackStepAggregate.TrackStep>.Filter.Where(x => x.UserId == request.Id.StringToInt() && x.InsertDate == request.Date);
        var track = await _work.GenericRepository<Domain.Aggregates.TrackStepAggregate.TrackStep>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return track.ToDto<UserStepsDto>().ToList();
    }
}