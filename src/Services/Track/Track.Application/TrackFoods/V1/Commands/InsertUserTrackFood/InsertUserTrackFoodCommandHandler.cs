using Common.Constants.Food;
using EventBus.Messages.Contracts.Services.Foods.Food;
using EventBus.Messages.Contracts.Services.Foods.MeasureUnit;
using EventBus.Messages.Contracts.Services.Foods.PersonalFoods;
using MassTransit;
using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackFoodAggregate;
using Track.Domain.Aggregates.TrackNutrientAggregate;

namespace Track.Application.TrackFoods.V1.Commands.InsertUserTrackFood;

public class InsertUserTrackFoodCommandHandler : IRequestHandler<InsertUserTrackFoodCommand, UserTrackFoodDto>
{
    private readonly IUnitOfWork _work;
    // private readonly IRequestClient<GetMeasureUnit> _clientMeasureUnit;
    // private readonly IRequestClient<GetFood> _clientFood;
    // private readonly IRequestClient<GetPersonalFood> _clientPersonalFood;

    public InsertUserTrackFoodCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<UserTrackFoodDto> Handle(InsertUserTrackFoodCommand request, CancellationToken cancellationToken)
    {
        // string foodName = null;
        // if (!string.IsNullOrWhiteSpace(request.FoodId))
        // {
        //     var food = await _clientFood.GetResponse<GetFoodResult>(new GetFood()
        //     {
        //         Id = request.FoodId
        //     }, cancellationToken);
        //     if (food.Message.Name == null)
        //         throw new NotFoundException($"measureUnit not found{request.MeasureUnitId}");
        //     foodName = GetValueByPropertyName(food.Message.Name, request.Language);
        // }
        // else if (!string.IsNullOrWhiteSpace(request.PersonalFoodId))
        // {
        //     var personalFood = await _clientPersonalFood.GetResponse<GetPersonalFoodResult>(new GetPersonalFood()
        //     {
        //         Id = request.PersonalFoodId
        //     }, cancellationToken);
        //     if (personalFood.Message.Name == null)
        //         throw new NotFoundException($"measureUnit not found{request.MeasureUnitId}");
        //     foodName = personalFood.Message.Name;
        // }
        //
        // var measureUnit = await _clientMeasureUnit.GetResponse<GetMeasureUnitResult>(new GetMeasureUnit()
        // {
        //     Id = request.MeasureUnitId
        // }, cancellationToken);
        // if (measureUnit.Message.Name == null)
        //     throw new NotFoundException($"measureUnit not found{request.MeasureUnitId}");
        var track = new TrackFood
        {
            InsertDate = request.InsertDate.DateTimeFormat(),
            FoodId = request.FoodId.StringToObjectId(),
            FoodMeal = request.FoodMeal,
            FoodNutrientValue = request.FoodNutrientValue.CreateNotNegativeForDecimalType(),
            Value = request.Value.CreateNotNegativeForDecimalType(),
            MeasureUnitId = request.MeasureUnitId.StringToObjectId(),
            PersonalFoodId = request.PersonalFoodId.StringToObjectId(),
            AppId = request.AppId,
            MeasureUnitName = request.MeasureUnitName,
            FoodName = request.FoodName,
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
            CreatedById = request.UserId.StringToInt(),
        };
        if (userNutrient != null)
        {
            var newNutrients = request.FoodNutrientValue;
            var lastNutrients = userNutrient.Value;
            var nutVal = new List<decimal>();
            for (int i = 0; i < lastNutrients.Count(); i++)
            {
                nutVal.Add(newNutrients[i] + lastNutrients[i]);
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
            MeasureUnitName = track.MeasureUnitName,
            Value = track.Value.Value,
            FoodMeal = track.FoodMeal,
            FoodNutrientValue = track.FoodNutrientValue.GetValueNotNegativeForDecimalType(),
            InsertDate = track.InsertDate,
            UserId = track.CreatedById.ToString() ?? string.Empty,
            AppId = track.AppId
        };
    }

    // private string GetValueByPropertyName<T>(T obj, string propertyName)
    // {
    //     PropertyInfo propInfo = typeof(T).GetProperty(propertyName);
    //
    //     return propInfo.GetValue(obj).ToString();
    // }
}