using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Interfaces.Services;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.FavoriteMealAggregate;

namespace Track.Application.FavoriteMeals.V1.Commands.PartialUpdateFavoriteMeals;

public class PatchFavoriteMealFoodsCommandHandler : IRequestHandler<PatchFavoriteMealFoodsCommand>
{
    private readonly IUnitOfWork _work;

    // private readonly IRequestClient<GetFood> _clientFood;
    private readonly IFileService _fileService;

    public PatchFavoriteMealFoodsCommandHandler(IUnitOfWork work, IFileService fileService)
    {
        _work = work;
        _fileService = fileService;
        // _clientFood = clientFood;
    }

    public async Task Handle(PatchFavoriteMealFoodsCommand request, CancellationToken cancellationToken)
    {
        // var food = await _clientFood.GetResponse<GetFoodResult>(new GetFood()
        // {
        //     Ids = id
        // }, cancellationToken);
        // if (food.Message.Foods.Count <= 0)
        //     throw new NotFoundException($"food not found");
        
        var filter = Builders<FavoriteMeal>.Filter.Eq(x => x.UserId, request.UserId.StringToInt());
        filter &= Builders<FavoriteMeal>.Filter.Eq(x => x.Meal, request.Meal);
        var track = await _work.GenericRepository<FavoriteMeal>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (track == null) throw new NotFoundException("track Not Found");
        track.Foods.AddRange(request.Foods.Select(x => new FoodMealEntity
        {
            FoodMeal = x.FoodMeal,
            FoodId = x.FoodId!.StringToObjectId(),
            Id = x.Id,
            Name = x.Name,
            Value = x.Value.CreateNotNegativeForDecimalType(),
            AppId = x.AppId,
            IsDelete = false,
            NutrientValue = x.NutrientValue.CreateNotNegativeForDecimalType(),
            MeasureUnitId = x.MeasureUnitId.StringToObjectId(),
            MeasureUnitName = x.MeasureUnitName,
            PersonalFoodId = x.PersonalFoodId!.StringToObjectId()
        }).ToList());
        track.CalorieValue = request.CalorieValue.CreateNotNegativeForDecimalType();
        track.Meal = request.Meal;
        if (!string.IsNullOrWhiteSpace(request.ImageUri))
        {
            track.ImageName = _fileService.AddImage(request.ImageUri, "FavoriteMealImage",
                Guid.NewGuid().ToString().Substring(0, 6));
        }

        await _work.GenericRepository<FavoriteMeal>()
            .UpdateOneAsync(x => x.Id == track.Id, track,
                new Expression<Func<FavoriteMeal, object>>[]
                {
                    x => x.Foods,
                    x => x.Meal,
                    x => x.CalorieValue,
                    x => x.ImageName
                }, null, cancellationToken);
    }
}