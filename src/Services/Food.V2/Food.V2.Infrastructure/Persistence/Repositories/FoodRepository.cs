using Food.V2.Application.Common.Exceptions;
using Food.V2.Application.Dtos.Food;
using Food.V2.Domain.Aggregates.FoodAggregate;
using Food.V2.Domain.Aggregates.RecipeAggregate;
using Newtonsoft.Json;

namespace Food.V2.Infrastructure.Persistence.Repositories;

public class FoodRepository : GenericRepository<Domain.Aggregates.FoodAggregate.Food>, IFoodRepository
{
    public FoodRepository(IMediator mediator, IConfiguration configuration, ICurrentUserService currentUserService) :
        base(mediator, configuration, currentUserService)
    {
    }

    public async Task<FoodTranslation?> GetFoodTranslationById(string id,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var translation = await Collection.AsQueryable().Where(x => x.Id == id && !x.IsDelete && x.UseInDiet).Select(s => s.Name)
            .FirstOrDefaultAsync(cancellationToken);
        return translation;
    }

    public async Task<List<string>?> GetFoodIngredientIdsById(string id,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var ingredientIds = await Collection.AsQueryable().Where(x => x.Id == id && !x.IsDelete)
            .Select(s => s.FoodIngredients).FirstOrDefaultAsync(cancellationToken);
        if (ingredientIds == null)
            throw new NotFoundException(nameof(Domain.Aggregates.FoodAggregate.Food), id);

        return ingredientIds.Select(s => s.ToString()).ToList();
    }

    public async Task<FoodWithDetailDto> GetFoodWithDetailById(string id, CancellationToken cancellationToken)
    {
        var foodBson = await Collection.Aggregate().Match(x => x.Id == id)
            .Lookup("recipes", "_id", "foodId", "RecipeResult")
            .Lookup("dietCategorys", "foodId", "_id", "DietCategoryResult")
            .Lookup("categorys", "foodId", "_id", "FoodCategoryResult")
            .Lookup("nationalitys", "foodId", "_id", "NationalityResult")
            .Lookup("measureUnits", "foodId", "_id", "DefaultMeasureUnitResult")
            .Lookup("measureUnits", "foodId", "_id", "MeasureUnitResult")
            .Lookup("ingredients", "foodId", "_id", "IngredientResult")
            .Lookup("recipeCategorys", "_id", "foodId", "RecipeCategoryResult")
            .Lookup("brands", "_id", "foodId", "BrandResult")
            .FirstOrDefaultAsync(cancellationToken);
        var food = BsonSerializer.Deserialize<FoodWithDetailDto>(foodBson);
        var s =JsonConvert.DeserializeObject<Dictionary<string, object>>(foodBson.Elements.FirstOrDefault(x=>x.Name.Equals("RecipeResult")).ToJson());
        return food;
    }
}