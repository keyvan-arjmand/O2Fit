using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Interfaces.Services;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.FavoriteMealAggregate;
using Track.Domain.ValueObjects;

namespace Track.Application.FavoriteMeals.V1.Commands.InsertFavoriteMeals;

public class InsertFavoriteMealsCommandHandler : IRequestHandler<InsertFavoriteMealsCommand, TrackMealDto>
{
    private readonly IUnitOfWork _work;

    // private readonly IRequestClient<GetFood> _clientFood;
    private readonly IFileService _fileService;

    public InsertFavoriteMealsCommandHandler(IUnitOfWork work, IFileService fileService)
    {
        _work = work;
        _fileService = fileService;
        // _clientFood = clientFood;
    }

    public async Task<TrackMealDto> Handle(InsertFavoriteMealsCommand request, CancellationToken cancellationToken)
    {
        // var filter = Builders<FavoriteMeal>.Filter.Eq(x => x.UserId, ObjectId.Parse(request.UserId));
        // filter &= Builders<FavoriteMeal>.Filter.Eq(x => x.Meal, request.Meal);
        // var lastTrack = await _work.GenericRepository<FavoriteMeal>()
        //     .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        // if (lastTrack != null) throw new AppException($"Track Meals already exist {lastTrack.Id}");
        // var food = await _clientFood.GetResponse<GetFoodResult>(new GetFood()
        // {
        //     Ids = request.FoodIds
        // }, cancellationToken);
        // if (food.Message.Foods.Count <= 0)
        //     throw new NotFoundException($"food not found");

        var track = new FavoriteMeal
        {
            UserId =request.UserId.StringToInt(),
            Meal = request.Meal,
            Foods = request.TrackFoods.Select(x => new FoodMealEntity
            {
                FoodMeal = x.FoodMeal,
                FoodId = x.FoodId!.StringToObjectId(),
                Id = x.Id,
                Name = x.Name,
                Value = new NotNegativeForDecimalTypes(x.Value),
                AppId = x.AppId,
                IsDelete = false,
                NutrientValue = x.NutrientValue.CreateNotNegativeForDecimalType(),
                MeasureUnitId = x.MeasureUnitId.StringToObjectId(),
                MeasureUnitName = x.MeasureUnitName,
                PersonalFoodId = x.PersonalFoodId!.StringToObjectId(),
            }).ToList(),
            CalorieValue = request.CalorieValue.CreateNotNegativeForDecimalType(),
            AppId = request.AppId,
        };
     
        if (!string.IsNullOrWhiteSpace(request.ImageUri))
        {
            track.ImageName = _fileService.AddImage(request.ImageUri, "FavoriteMealImage",
                Guid.NewGuid().ToString().Substring(0, 6));
        }

        await _work.GenericRepository<FavoriteMeal>()
            .InsertOneAsync(track, null, cancellationToken);
        return track.ToDto<TrackMealDto>();
    }
}