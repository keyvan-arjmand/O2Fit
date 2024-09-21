using Food.V2.Application.Dtos.DietPack;

namespace Food.V2.Infrastructure.Persistence.Repositories;

public class DietPackAggregation : GenericRepository<DietPack> ,IDietPackAggregation
{
    public DietPackAggregation(IMediator mediator, IConfiguration configuration, ICurrentUserService currentUserService) : base(mediator, configuration, currentUserService)
    {
    }

    public async Task<List<GetUserPackageAggregationResultDto>> GetUserPackageAsync(string dietPackCategoryId, int dailyCalorie, List<string>? allergyIds,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var data = new List<GetUserPackageAggregationResultDto>();
        var allergyObjectIds = new List<ObjectId>();
        if(allergyIds != null || allergyIds?.Count > 0)
        {
            allergyObjectIds = allergyIds.Select(ObjectId.Parse).ToList();
            
            var dietPackWithAllergyBson = await  Collection.Aggregate().Match(x =>
                    x.DietCategoryIds.Contains(ObjectId.Parse(dietPackCategoryId)) &&
                    x.DailyCalorie == dailyCalorie && !allergyObjectIds.Any(c=>x.IngredientAllergy.Contains(c)))
                
                
                .Lookup("measureUnits", "dietPackFoods.measureUnitId", "_id", "measureUnits")
                .Lookup("foods", "dietPackFoods.foodId", "_id", "foods")
                .ToListAsync(cancellationToken);
            var dietPackWithAllergyJson = dietPackWithAllergyBson.ToJson();
            data = BsonSerializer.Deserialize<List<GetUserPackageAggregationResultDto>>(dietPackWithAllergyJson);
        }
        else
        {
         
            var dietPackWithOutAllergyBson = await  Collection.Aggregate().Match(x =>
                    x.DietCategoryIds.Contains(ObjectId.Parse(dietPackCategoryId)) &&
                    x.DailyCalorie == dailyCalorie)
                .Lookup("measureUnits", "dietPackFoods.measureUnitId", "_id", "measureUnits")
                .Lookup("foods", "dietPackFoods.foodId", "_id", "foods")
                .ToListAsync(cancellationToken);
            var dietPackWithOutAllergyJson = dietPackWithOutAllergyBson.ToJson();
            data = BsonSerializer.Deserialize<List<GetUserPackageAggregationResultDto>>(dietPackWithOutAllergyJson);   
        }
        return data;
    }
}