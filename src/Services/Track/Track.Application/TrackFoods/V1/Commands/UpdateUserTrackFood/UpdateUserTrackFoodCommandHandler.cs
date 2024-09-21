using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackFoodAggregate;
using Track.Domain.Aggregates.TrackNutrientAggregate;

namespace Track.Application.TrackFoods.V1.Commands.UpdateUserTrackFood;

public class UpdateUserTrackFoodCommandHandler : IRequestHandler<UpdateUserTrackFoodCommand, UserTrackFoodDto>
{
    private readonly IUnitOfWork _work;

    public UpdateUserTrackFoodCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<UserTrackFoodDto> Handle(UpdateUserTrackFoodCommand request, CancellationToken cancellationToken)
    {
        var lastTrack = await _work.GenericRepository<TrackFood>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (lastTrack == null) throw new NotFoundException($"track Not Found {request.Id}");

        lastTrack.FoodNutrientValue = request.FoodNutrientValue.CreateNotNegativeForDecimalType();
        lastTrack.FoodMeal = request.FoodMeal;
        lastTrack.FoodName = request.FoodName;
        lastTrack.MeasureUnitName = request.MeasureUnitName;

        await _work.GenericRepository<TrackFood>()
            .UpdateOneAsync(x => x.Id == request.Id, lastTrack,
                new Expression<Func<TrackFood, object>>[]
                {
                    x => x.Value,
                    x => x.InsertDate,
                    x => x.CreatedById,
                    x => x.FoodName,
                    x => x.MeasureUnitName,
                }, null, cancellationToken);
        //Track Nutrient
        if (lastTrack.InsertDate == request.InsertDate.DateTimeFormat())
        {
            var filter = Builders<TrackNutrient>.Filter.Eq(x => x.CreatedById, request.UserId.StringToInt());
            filter &= Builders<TrackNutrient>.Filter.Eq(x => x.InsertDate, request.InsertDate.DateTimeFormat());
            var userNutrient = await _work.GenericRepository<TrackNutrient>()
                .GetSingleDocumentByFilterAsync(filter, cancellationToken);

            userNutrient.InsertDate = lastTrack.InsertDate.DateTimeFormat();
            userNutrient.CreatedById = request.UserId.StringToInt();
            var newNutrients = request.FoodNutrientValue;
            var lastNutrients = userNutrient.Value;
            var nutVal = new List<decimal>();
            for (int i = 0; i < lastNutrients.Count(); i++)
            {
                decimal value = newNutrients[i].CreateNotNegativeForDecimalType() + lastNutrients[i].GetNotNegativeForDecimalType() - lastTrack.FoodNutrientValue[i];
                decimal nutValue = (value > 0) ? value : 0;
                nutVal.Add(nutValue);
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
        }
        else
        {
            var filter = Builders<TrackNutrient>.Filter.Eq(x => x.CreatedById, request.UserId.StringToInt());
            filter &= Builders<TrackNutrient>.Filter.Eq(x => x.InsertDate, lastTrack.InsertDate);
            var userNutrient = await _work.GenericRepository<TrackNutrient>()
                .GetSingleDocumentByFilterAsync(filter, cancellationToken);

            userNutrient.InsertDate = lastTrack.InsertDate;
            userNutrient.CreatedById =request.UserId.StringToInt();
            var foodNutrients = lastTrack.FoodNutrientValue;
            var lastNutrients = userNutrient.Value;
            var nutVal = new List<decimal>();
            for (int i = 0; i < lastNutrients.Count(); i++)
            {
                var value = lastNutrients[i] - foodNutrients[i];
                decimal nutValue = (value.CreateNotNegativeForDecimalType() > 0) ? value.CreateNotNegativeForDecimalType() : 0;
                nutVal.Add(nutValue);
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
            // if insert Date Change
            // add user Track Nutrient
            var filterNutrient = Builders<TrackNutrient>.Filter.Eq(x => x.CreatedById, request.UserId.StringToInt());
            filterNutrient &= Builders<TrackNutrient>.Filter.Eq(x => x.InsertDate, request.InsertDate);
            var userNutrientCurrent = await _work.GenericRepository<TrackNutrient>()
                .GetSingleDocumentByFilterAsync(filterNutrient, cancellationToken);
            if (userNutrientCurrent != null)
            {
                userNutrientCurrent.InsertDate = request.InsertDate;
                userNutrientCurrent.CreatedById = request.UserId.StringToInt();
                var newNutrients = request.FoodNutrientValue;
                lastNutrients = userNutrientCurrent.Value;
                nutVal = new List<decimal>();
                for (int i = 0; i < lastNutrients.Count(); i++)
                {
                    nutVal.Add(newNutrients[i] + lastNutrients[i]);
                }

                userNutrientCurrent.Value = nutVal.CreateNotNegativeForDecimalType();
                await _work.GenericRepository<TrackNutrient>()
                    .UpdateOneAsync(x => x.Id == userNutrientCurrent.Id, userNutrientCurrent,
                        new Expression<Func<TrackNutrient, object>>[]
                        {
                            x => x.Value,
                            x => x.InsertDate,
                            x => x.CreatedById,
                        }, null, cancellationToken);
            }
            else
            {
                await _work.GenericRepository<TrackNutrient>().InsertOneAsync(new TrackNutrient
                {
                    InsertDate = request.InsertDate,
                    Value = lastTrack.FoodNutrientValue,
                    AppId = request.AppId
                }, null, cancellationToken);
            }
        }

        return new UserTrackFoodDto
        {
            Id = lastTrack.Id,
            FoodId = lastTrack.FoodId.ToString(),
            PersonalFoodId = lastTrack.PersonalFoodId.ToString(),
            FoodName = lastTrack.FoodName,
            MeasureUnitId = lastTrack.MeasureUnitId.ToString() ?? string.Empty,
            MeasureUnitName = lastTrack.MeasureUnitName,
            Value = lastTrack.Value,
            FoodMeal = lastTrack.FoodMeal,
            FoodNutrientValue = lastTrack.FoodNutrientValue.GetValueNotNegativeForDecimalType(),
            InsertDate = lastTrack.InsertDate,
            UserId = lastTrack.CreatedById.ToString() ?? string.Empty,
            AppId = lastTrack.AppId
        };
    }
}