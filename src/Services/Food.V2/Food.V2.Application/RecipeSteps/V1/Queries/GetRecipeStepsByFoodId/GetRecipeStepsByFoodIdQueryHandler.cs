using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.RecipeSteps.V1.Queries.GetRecipeStepsByFoodId;

public class GetRecipeStepsByFoodIdQueryHandler : IRequestHandler<GetRecipeStepsByFoodIdQuery, List<TranslationDto>>
{
    private readonly IUnitOfWork _work;

    public GetRecipeStepsByFoodIdQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<TranslationDto>> Handle(GetRecipeStepsByFoodIdQuery request,
        CancellationToken cancellationToken)
    {
        var food = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (food == null) throw new NotFoundException($"food not found {request.Id}");
        return food.RecipeSteps.MapTo<List<TranslationDto>, List<FoodTranslation>>().ToList();
    }
}