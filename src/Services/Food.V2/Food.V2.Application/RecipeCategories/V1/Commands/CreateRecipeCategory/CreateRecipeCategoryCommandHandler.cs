namespace Food.V2.Application.RecipeCategories.V1.Commands.CreateRecipeCategory;

public class CreateRecipeCategoryCommandHandler : IRequestHandler<CreateRecipeCategoryCommand , string>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;
    public CreateRecipeCategoryCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task<string> Handle(CreateRecipeCategoryCommand request, CancellationToken cancellationToken)
    {
        var recipeCategory = new RecipeCategory
        {
            ImageName = _fileService.AddImage(request.ImageUri,"RecipeCategory",Guid.NewGuid().ToString()),
            Translation = request.TranslationDto.ToEntity<RecipeCategoryTranslation>()
        };

        await _uow.GenericRepository<RecipeCategory>().InsertOneAsync(recipeCategory, null, cancellationToken);

        return recipeCategory.Id;
    }
}