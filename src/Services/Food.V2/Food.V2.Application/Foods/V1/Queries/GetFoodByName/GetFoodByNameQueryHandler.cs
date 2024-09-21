using System.Text.RegularExpressions;
using Common.Constants.Food;
using MongoDB.Driver.Linq;

namespace Food.V2.Application.Foods.V1.Queries.GetFoodByName;

public class GetFoodByNameQueryHandler : IRequestHandler<GetFoodByNameQuery, PaginationResult<SearchFoodNameDto>>
{
    private readonly IUnitOfWork _work;

    public GetFoodByNameQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<PaginationResult<SearchFoodNameDto>> Handle(GetFoodByNameQuery request,
        CancellationToken cancellationToken)
    {
        FilterDefinition<Domain.Aggregates.FoodAggregate.Food> filter = FilterDefinition<Domain.Aggregates.FoodAggregate.Food>.Empty;
        if (!string.IsNullOrWhiteSpace(request.FoodName))
        {
            switch (request.Lang)
            {
                case "Persian":
                    filter = new FilterDefinitionBuilder<Domain.Aggregates.FoodAggregate.Food>().Where(x =>
                        x.Name.Persian.ToLower().Contains(request.FoodName.ToLower()));
                    break;
                case "English":
                    filter = new FilterDefinitionBuilder<Domain.Aggregates.FoodAggregate.Food>().Where(x =>
                        x.Name.English.ToLower().Contains(request.FoodName.ToLower()));
                    break;
                case "Arabic":
                    filter = new FilterDefinitionBuilder<Domain.Aggregates.FoodAggregate.Food>().Where(x =>
                        x.Name.Arabic.ToLower().Contains(request.FoodName.ToLower()));
                    break;
                default:
                    filter = new FilterDefinitionBuilder<Domain.Aggregates.FoodAggregate.Food>().Where(x =>
                        x.Name.Persian.ToLower().Contains(request.FoodName.ToLower()));
                    break;
            }
        }
        // var filter = Builders<Domain.Aggregates.FoodAggregate.Food>.Filter.Regex(
        //     (Keys.TransactionNameKey + request.Lang),
        //     BsonRegularExpression.Create(new Regex(request.FoodName)));
        var foods = await _work.FoodAggregation()
            .GetListPaginationOfDocumentsByFilterAsync(request.Page, request.PageSize, filter, cancellationToken);
        foods.ForEach(x => x.Lang = request.Lang);
        var pagination = PaginationResult<SearchFoodNameDto>.CreatePaginationResult(request.Page, request.PageSize,
            foods.Count,
            foods.MapTo<List<SearchFoodNameDto>, List<FoodWithDetailDto>>());
        return pagination;
    }
}