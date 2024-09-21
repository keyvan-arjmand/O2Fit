namespace Food.V2.Application.RecipeCategories.V1.Commands.SoftDeleteRecipeCategory;

public class SoftDeleteRecipeCategoryCommandHandler : IRequestHandler<SoftDeleteRecipeCategoryCommand>
{
    private readonly IUnitOfWork _uow;

    public SoftDeleteRecipeCategoryCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(SoftDeleteRecipeCategoryCommand request, CancellationToken cancellationToken)
    {
        var recipeCategory = await _uow.GenericRepository<RecipeCategory>().GetByIdAsync(request.Id, cancellationToken);
        if (recipeCategory == null)
            throw new NotFoundException(nameof(RecipeCategory), request.Id);

        await _uow.GenericRepository<RecipeCategory>()
            .SoftDeleteByIdAsync(request.Id, recipeCategory, null, cancellationToken);
    }
}