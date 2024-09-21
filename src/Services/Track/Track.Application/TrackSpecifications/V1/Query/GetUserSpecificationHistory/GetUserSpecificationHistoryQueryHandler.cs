using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackSpecificationAggregate;

namespace Track.Application.TrackSpecifications.V1.Query.GetUserSpecificationHistory;

public class GetUserSpecificationHistoryQueryHandler : IRequestHandler<GetUserSpecificationHistoryQuery,
    List<UserTrackSpecificationDto>>
{
    private readonly IUnitOfWork _work;

    public GetUserSpecificationHistoryQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<UserTrackSpecificationDto>> Handle(GetUserSpecificationHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var userId = request.UserId.StringToInt();
        var filter = Builders<TrackSpecification>.Filter.Where(x =>
            x.InsertDate >= request.DateTime && x.CreatedById == userId);
        var result = await _work.GenericRepository<TrackSpecification>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return result.ToDto<UserTrackSpecificationDto>().ToList();
    }
}