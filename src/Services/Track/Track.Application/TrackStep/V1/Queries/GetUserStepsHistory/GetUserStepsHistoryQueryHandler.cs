using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;

namespace Track.Application.TrackStep.V1.Queries.GetUserStepsHistory;

public class GetUserStepsHistoryQueryHandler : IRequestHandler<GetUserStepsHistoryQuery, List<UserStepsDto>>
{
    private readonly IUnitOfWork _work;

    public GetUserStepsHistoryQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<UserStepsDto>> Handle(GetUserStepsHistoryQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.TrackStepAggregate.TrackStep>.Filter.Where(x => x.InsertDate <= DateTime.Now.AddDays(-request.Days) && x.UserId == request.Id.StringToInt());
        var track = await _work.GenericRepository<Domain.Aggregates.TrackStepAggregate.TrackStep>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return track.ToDto<UserStepsDto>().ToList();
    }
}