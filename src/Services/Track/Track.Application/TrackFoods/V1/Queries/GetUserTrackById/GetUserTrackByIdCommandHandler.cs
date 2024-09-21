using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackFoodAggregate;

namespace Track.Application.TrackFoods.V1.Queries.GetUserTrackById;

public class GetUserTrackByIdCommandHandler : IRequestHandler<GetUserTrackByIdCommand, UserTrackFoodDto>
{
    private readonly IUnitOfWork _work;

    public GetUserTrackByIdCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<UserTrackFoodDto> Handle(GetUserTrackByIdCommand request, CancellationToken cancellationToken)
    {
        var track = await _work.GenericRepository<TrackFood>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (track == null) throw new NotFoundException($"Track Not Found {request.Id}");
        return track!.ToDto<UserTrackFoodDto>();
    }
}