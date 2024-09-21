using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Domain.Aggregates.TrackFoodAggregate;
using Track.Domain.Aggregates.TrackNutrientAggregate;

namespace Track.Application.TrackFoods.V1.Commands.SoftDeleteTrackFood;

public class SoftDeleteTrackFoodCommandHandler : IRequestHandler<SoftDeleteTrackFoodCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteTrackFoodCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteTrackFoodCommand request, CancellationToken cancellationToken)
    {
        var track = await _work.GenericRepository<TrackFood>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (track == null) throw new NotFoundException($"track Not Found {request.Id}");
        track.IsDelete = true;
        // Track Nutrient
        var filter = Builders<TrackNutrient>.Filter.Eq(x => x.CreatedById, track.CreatedById);
        filter &= Builders<TrackNutrient>.Filter.Eq(x => x.InsertDate, track.InsertDate);
        var userNutrient = await _work.GenericRepository<TrackNutrient>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        var foodNutrients = track.FoodNutrientValue;
        var lastNutrients = userNutrient.Value;
        var nutVal = new List<decimal>();
        for (int i = 0; i < lastNutrients.Count(); i++)
        {
            var value = lastNutrients[i] - foodNutrients[i];
            decimal nutValue = (value.CreateNotNegativeForDecimalType() > 0) ? value.CreateNotNegativeForDecimalType() : 0;
            nutVal.Add(value);
        }

        userNutrient.Value = nutVal.CreateNotNegativeForDecimalType();
        await _work.GenericRepository<TrackNutrient>()
            .UpdateOneAsync(x => x.Id == userNutrient.Id, userNutrient,
                new Expression<Func<TrackNutrient, object>>[]
                {
                    x => x.Value,
                    x => x.InsertDate,
                    x => x.CreatedById,
                }, null, cancellationToken);

        await _work.GenericRepository<TrackFood>().SoftDeleteByIdAsync(request.Id, track, null, cancellationToken);
    }
}