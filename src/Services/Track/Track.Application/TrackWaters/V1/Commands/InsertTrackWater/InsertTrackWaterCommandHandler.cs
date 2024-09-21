using MongoDB.Bson;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackWaterAggregate;

namespace Track.Application.TrackWaters.V1.Commands.InsertTrackWater;

public class InsertTrackWaterCommandHandler : IRequestHandler<InsertTrackWaterCommand, UserTrackWaterDto>
{
    private readonly IUnitOfWork _work;

    public InsertTrackWaterCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<UserTrackWaterDto> Handle(InsertTrackWaterCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<TrackWater>.Filter.Where(x =>
            x.InsertDate == request.InsertDate && x.CreatedById == request.UserId.StringToInt());
        var result = await _work.GenericRepository<TrackWater>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (result != null)
        {
            result.Value = request.Value.CreateNotNegativeForDecimalType();
            await _work.GenericRepository<TrackWater>()
                .UpdateOneAsync(x => x.Id == result.Id, result,
                    new Expression<Func<TrackWater, object>>[]
                    {
                        x => x.Value,
                    }, null, cancellationToken);
            return result.ToDto<UserTrackWaterDto>();

        }
        else
        {
            var track = new TrackWater
            {
                Value = request.Value.CreateNotNegativeForDecimalType(),
                AppId = request.AppId,
                InsertDate = request.InsertDate,
            };
            await _work.GenericRepository<TrackWater>().InsertOneAsync(track, null, cancellationToken);
            return track.ToDto<UserTrackWaterDto>();
        }
    }
}