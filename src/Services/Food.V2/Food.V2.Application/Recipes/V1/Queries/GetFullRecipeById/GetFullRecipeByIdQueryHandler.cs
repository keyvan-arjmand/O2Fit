using Food.V2.Application.Dtos.Recipe;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.Recipes.V1.Queries.GetFullRecipeById;

public class GetFullRecipeByIdQueryHandler : IRequestHandler<GetFullRecipeByIdQuery, FullRecipeDto>
{
    private readonly IUnitOfWork _work;

    public GetFullRecipeByIdQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<FullRecipeDto> Handle(GetFullRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var food = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (food == null) throw new NotFoundException($"food not found {request.Id}");
        return food.ToDto<FullRecipeDto>();
    }
}