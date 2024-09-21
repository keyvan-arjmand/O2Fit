using Food.V2.Application.Dtos.Recipe;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.RecipeAggregate;
using Google.Protobuf.Collections;

namespace Food.V2.Application.RecipeSteps.V1.Queries.GetAllRecipeSteps;

public class GetAllRecipeStepsQueryHandler : IRequestHandler<GetAllRecipeStepsQuery, List<ListRecipeStepDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllRecipeStepsQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<ListRecipeStepDto>> Handle(GetAllRecipeStepsQuery request,
        CancellationToken cancellationToken)
    {
        var food = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>().GetAllAsync(cancellationToken);
        return food.MapTo<List<ListRecipeStepDto>, List<Domain.Aggregates.FoodAggregate.Food>>();
    }
}