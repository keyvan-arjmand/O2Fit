namespace Food.V2.Application.RecipeCategories.V1.Commands.UpdateRecipeCategory;

public class UpdateRecipeCategoryCommandHandler : IRequestHandler<UpdateRecipeCategoryCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;

    public UpdateRecipeCategoryCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task Handle(UpdateRecipeCategoryCommand request, CancellationToken cancellationToken)
    {
        var recipeCategory = await _uow.GenericRepository<RecipeCategory>().GetByIdAsync(request.Id, cancellationToken);
        if (recipeCategory == null)
            throw new NotFoundException(nameof(RecipeCategory), request.Id);

        if (!string.IsNullOrEmpty(recipeCategory.ImageName))
        {
            _fileService.RemoveImage(recipeCategory.ImageName,"RecipeCategory");
        }

        recipeCategory.ImageName =
            _fileService.AddImage(request.ImageUri, "RecipeCategory", ObjectId.GenerateNewId().ToString());

        recipeCategory.Translation.Persian = request.TranslationDto.Persian;
        recipeCategory.Translation.English = request.TranslationDto.English;
        recipeCategory.Translation.Arabic = request.TranslationDto.Arabic;

        await _uow.GenericRepository<RecipeCategory>().UpdateOneAsync(x => x.Id == request.Id, recipeCategory,
            new Expression<Func<RecipeCategory, object>>[]
            {
                x => x.Translation.English,
                x => x.Translation.Arabic,
                x => x.Translation.Persian,
                x => x.ImageName
            }, null, cancellationToken);

    }
}