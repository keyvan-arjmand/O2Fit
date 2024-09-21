using Food.V2.Application.Dtos.Recipe;
using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.Recipes.V1.Queries.GetByRecipeById;

public class GetByRecipeByIdQueryHandler : IRequestHandler<GetByRecipeByIdQuery, RecipeDto>
{
    private readonly IUnitOfWork _work;

    public GetByRecipeByIdQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<RecipeDto> Handle(GetByRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var recipe = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (recipe == null) throw new NotFoundException("food not found");
        return recipe.ToDto<RecipeDto>();
    }
}