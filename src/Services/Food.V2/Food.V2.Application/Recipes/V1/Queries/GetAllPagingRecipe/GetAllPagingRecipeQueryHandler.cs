using Food.V2.Application.Dtos.Recipe;
using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.Recipes.V1.Queries.GetAllPagingRecipe;

public class GetAllPagingRecipeQueryHandler : IRequestHandler<GetAllPagingRecipeQuery, PaginationResult<RecipeDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllPagingRecipeQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<PaginationResult<RecipeDto>> Handle(GetAllPagingRecipeQuery request,
        CancellationToken cancellationToken)
    {
        var recipe = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetAllPaginationAsync(request.Page, request.PageSize, cancellationToken);
        var dtoResult = recipe.Data.ToDto<RecipeDto>().ToList();
        return PaginationResult<RecipeDto>.CreatePaginationResult(request.Page, request.PageSize,
            dtoResult.Count, dtoResult);
    }
}