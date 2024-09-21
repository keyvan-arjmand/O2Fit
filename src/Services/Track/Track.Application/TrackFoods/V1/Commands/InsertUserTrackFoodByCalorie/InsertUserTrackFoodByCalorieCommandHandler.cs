using Common.Constants.Food;
//using Track.Domain.Enums;

using MongoDB.Bson;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackFoodAggregate;
using Track.Domain.Aggregates.TrackNutrientAggregate;

namespace Track.Application.TrackFoods.V1.Commands.InsertUserTrackFoodByCalorie;

public class
    InsertUserTrackFoodByCalorieCommandHandler : IRequestHandler<InsertUserTrackFoodByCalorieCommand, UserTrackFoodDto>
{
    private readonly IUnitOfWork _work;

    public InsertUserTrackFoodByCalorieCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<UserTrackFoodDto> Handle(InsertUserTrackFoodByCalorieCommand request,
        CancellationToken cancellationToken)
    {
        var track = new TrackFood
        {
            InsertDate = request.InsertDate.DateTimeFormat(),
            FoodMeal = request.FoodMeal,
            FoodNutrientValue = request.FoodNutrientValue.CreateNotNegativeForDecimalType(),
            Value = request.FoodNutrientValue[23].CreateNotNegativeForDecimalType(),
            AppId = request.AppId,
            FoodName = request.FoodName,
            MeasureUnitId = Keys.MeasureUnitGrams.StringToObjectId(),
            MeasureUnitName = request.MeasureUnitName,
        };
        await _work.GenericRepository<TrackFood>().InsertOneAsync(track, null, cancellationToken);
        // Track Nutrient
        var filter = Builders<TrackNutrient>.Filter.Eq(x => x.CreatedById, request.UserId.StringToInt());
        filter &= Builders<TrackNutrient>.Filter.Eq(x => x.InsertDate, request.InsertDate);
        var userNutrient = await _work.GenericRepository<TrackNutrient>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        var userTrackNutrient = new TrackNutrient
        {
            InsertDate = request.InsertDate.DateTimeFormat(),
            CreatedById =request.UserId.StringToInt(),
        };
        if (userNutrient != null)
        {
            var newNutrients = request.FoodNutrientValue;
            var lastNutrients = userNutrient.Value;
            var nutVal = new List<decimal>();
            for (int i = 0; i < lastNutrients.Count(); i++)
            {
                nutVal.Add(newNutrients[i].CreateNotNegativeForDecimalType() + lastNutrients[i]);
            }
            
            userTrackNutrient.Value = nutVal.CreateNotNegativeForDecimalType();
            userTrackNutrient.Id = userNutrient.Id;
            userTrackNutrient.AppId = userNutrient.AppId;

            await _work.GenericRepository<TrackNutrient>()
                .UpdateOneAsync(x => x.Id == userTrackNutrient.Id, userTrackNutrient,
                    new Expression<Func<TrackNutrient, object>>[]
                    {
                        x => x.Value,
                        x => x.InsertDate,
                        x => x.CreatedById,
                    }, null, cancellationToken);
        }
        else
        {
            userTrackNutrient.AppId = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            userTrackNutrient.Value = track.FoodNutrientValue;
            await _work.GenericRepository<TrackNutrient>().InsertOneAsync(userTrackNutrient, null, cancellationToken);
        }

        return new UserTrackFoodDto
        {
            Id = track.Id,
            FoodId = track.FoodId.ToString(),
            PersonalFoodId = track.PersonalFoodId.ToString(),
            FoodName = track.FoodName,
            MeasureUnitId = track.MeasureUnitId.ToString() ?? string.Empty,
            MeasureUnitName = track.MeasureUnitName ,
            Value = track.Value,
            FoodMeal = track.FoodMeal,
            FoodNutrientValue = track.FoodNutrientValue.GetValueNotNegativeForDecimalType(),
            InsertDate = track.InsertDate,
            UserId = track.CreatedById.ToString() ?? string.Empty,
            AppId = track.AppId
        };
    }
}