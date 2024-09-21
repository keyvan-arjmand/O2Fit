using Food.V2.Application.Dtos.Recipe;
using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.Foods.V1.Queries.GetAllActiveFoodQuery;

public class
    GetAllActiveRecipeFoodsQueryHandler : IRequestHandler<GetAllActiveRecipeFoodsQuery, List<FullRecipeDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllActiveRecipeFoodsQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<FullRecipeDto>> Handle(GetAllActiveRecipeFoodsQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.FoodAggregate.Food>.Filter.Eq(x => x.IsActive, true);
        var foods = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return foods.ToDto<FullRecipeDto>().ToList();
    }
}