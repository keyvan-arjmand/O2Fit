namespace Food.V2.Application.RecipeCategories.V1.Queries.GetByIdRecipeCategory;

public class GetByIdRecipeCategoryQueryHandler : IRequestHandler<GetByIdRecipeCategoryQuery, RecipeCategoryDto>
{
    private readonly IUnitOfWork _uow;

    public GetByIdRecipeCategoryQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<RecipeCategoryDto> Handle(GetByIdRecipeCategoryQuery request, CancellationToken cancellationToken)
    {
        var recipeCategory = await _uow.GenericRepository<RecipeCategory>().GetByIdAsync(request.Id, cancellationToken);
        if (recipeCategory == null)
            throw new NotFoundException(nameof(RecipeCategory), request.Id);

        return recipeCategory.ToDto<RecipeCategoryDto>();
    }
}