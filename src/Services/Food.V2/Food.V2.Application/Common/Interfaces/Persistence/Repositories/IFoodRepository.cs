namespace Food.V2.Application.Common.Interfaces.Persistence.Repositories;

public interface IFoodRepository : IGenericRepository<Domain.Aggregates.FoodAggregate.Food>
{
    Task<FoodTranslation?> GetFoodTranslationById(string id,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<List<string>?> GetFoodIngredientIdsById(string id,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<FoodWithDetailDto> GetFoodWithDetailById(string id, CancellationToken cancellationToken);


}