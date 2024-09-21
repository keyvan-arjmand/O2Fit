using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackSpecificationAggregate;

namespace Track.Application.TrackSpecifications.V1.Query.GetLastUserSpecificationByIndex;

public class
    GetLastUserSpecificationByIndexQueryHandler : IRequestHandler<GetLastUserSpecificationByIndexQuery,
        UserTrackSpecificationDto>
{
    private readonly IUnitOfWork _work;

    public GetLastUserSpecificationByIndexQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<UserTrackSpecificationDto> Handle(GetLastUserSpecificationByIndexQuery request,
        CancellationToken cancellationToken)
    {
        var filter =
            Builders<TrackSpecification>.Filter.Where(x =>
                !string.IsNullOrEmpty(x.Image) || !string.IsNullOrEmpty(x.Note));
        var result = await _work.TrackSpecificationRepository()
            .GetPaginationTrackUserSpecification(request.Index + 1, filter, cancellationToken);
        if (result.Count != request.Index + 1) throw new NotFoundException("Track Not Found");
        return result[request.Index].ToDto<UserTrackSpecificationDto>();
    }
}