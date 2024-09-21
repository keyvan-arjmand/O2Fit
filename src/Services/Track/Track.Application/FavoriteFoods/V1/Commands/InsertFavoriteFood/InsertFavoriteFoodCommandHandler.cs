using EventBus.Messages.Contracts.Services.Foods.Food;
using MassTransit;
using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.FavoriteFoodAggregate;
using Track.Domain.Aggregates.FavoriteMealAggregate;

namespace Track.Application.FavoriteFoods.V1.Commands.InsertFavoriteFood;

public class InsertFavoriteFoodCommandHandler : IRequestHandler<InsertFavoriteFoodCommand, FavoriteFoodGetDto>
{
    private readonly IUnitOfWork _work;
    private readonly IRequestClient<GetFood> _clientFood;

    public InsertFavoriteFoodCommandHandler(IUnitOfWork work, IRequestClient<GetFood> clientFood)
    {
        _work = work;
        _clientFood = clientFood;
    }

    public async Task<FavoriteFoodGetDto> Handle(InsertFavoriteFoodCommand request, CancellationToken cancellationToken)
    {
        var food = await _clientFood.GetResponse<GetFoodResult>(new GetFood()
        {
            Id = request.FoodId
        }, cancellationToken);
        if (string.IsNullOrWhiteSpace(food.Message.Id))
            throw new NotFoundException($"Food not found{request.FoodId}");
        var favFood = new FavoriteFood
        {
            FoodName = new FavoriteFoodTranslation
            {
                Persian = food.Message.FoodNamePersian,
                English = food.Message.FoodNameEnglish,
                Arabic = food.Message.FoodNameArabic,
            },
            AppId = request.AppId,
            FoodId = food.Message.Id,
            NutrientValue = food.Message.NutrientValue.CreateNotNegativeForDecimalType()
        };
        await _work.GenericRepository<FavoriteFood>()
            .InsertOneAsync(favFood, null, cancellationToken);
        return new FavoriteFoodGetDto
        {
            AppId = favFood.AppId,
            UserId = favFood.CreatedById.ToString()!,
            FoodId = favFood.FoodId,
            Name = favFood.FoodName.GetNameInTranslation(request.Lang)
        };
    }
}