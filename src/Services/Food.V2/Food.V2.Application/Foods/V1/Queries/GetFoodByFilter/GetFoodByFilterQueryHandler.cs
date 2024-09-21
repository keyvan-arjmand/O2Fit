using Common.Constants.Food;
using Food.V2.Application.Dtos.Recipe;
using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.Foods.V1.Queries.GetFoodByFilter;

public class GetFoodByFilterQueryHandler : IRequestHandler<GetFoodByFilterQuery, List<FilterFoodDto>>
{
    private readonly IUnitOfWork _work;

    public GetFoodByFilterQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<FilterFoodDto>> Handle(GetFoodByFilterQuery request, CancellationToken cancellationToken)
    {
        List<FilterFoodDto> foods = await FilterFood(request.Filter);
        if (foods.Count <= 0) throw new NotFoundException("foods not found");
        return foods;
    }

    private async Task<List<FilterFoodDto>> FilterFood(FoodSearchFilter filters)
    {
        var builder = Builders<Domain.Aggregates.FoodAggregate.Food>.Filter;
        FilterDefinition<Domain.Aggregates.FoodAggregate.Food> filter = builder.Empty;
        if (!string.IsNullOrWhiteSpace(filters.Id))
        {
            filter &= builder.Eq(x => x.Id, filters.Id);
        }

        if (!string.IsNullOrWhiteSpace(filters.FoodCode))
        {
            filter &= builder.Eq(x => x.FoodCode, filters.FoodCode);
        }

        if (!string.IsNullOrWhiteSpace(filters.Name))
        {
            filter &= builder.Regex(Keys.TransactionKey + filters.Language, new BsonRegularExpression(filters.Name));
        }

        if (filters.RecipeStatus != null)
        {
            filter &= builder.Eq(x => x.RecipeStatus, filters.RecipeStatus);
        }

        if (filter != null)
        {
            var result = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
                .GetListOfDocumentsByFilterAsync(filter);
            return result.ToDto<FilterFoodDto>().ToList();
        }
        else
        {
            var result = await _work.FoodAggregation()
                .GetAllPaginationAsync(filters.Page, filters.PageSize);
            return result.Data.ToDto<FilterFoodDto>().ToList();
        }
    }
}