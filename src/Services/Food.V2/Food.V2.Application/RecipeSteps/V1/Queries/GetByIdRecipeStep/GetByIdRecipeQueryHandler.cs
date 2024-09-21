using Food.V2.Application.Dtos.Recipe;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.RecipeSteps.V1.Queries.GetByIdRecipeStep;

public class GetByIdRecipeQueryHandler : IRequestHandler<GetByIdRecipeQuery, RecipeStepDto>
{
    private readonly IUnitOfWork _work;

    public GetByIdRecipeQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<RecipeStepDto> Handle(GetByIdRecipeQuery request, CancellationToken cancellationToken)
    {
        var food = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.FoodId, cancellationToken);
        if (food == null) throw new NotFoundException($"food not found {request.FoodId}");
        var step = food.RecipeSteps.FirstOrDefault(x => x.Id == request.Id)            ;
        if (step == null) throw new NotFoundException("Not Found step");
        return new RecipeStepDto
        {
            FoodId = food.Id,
            RecipeStep = step.MapTo<TranslationDto, FoodTranslation>(),
        };
    }
}