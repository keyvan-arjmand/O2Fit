namespace Food.V2.Application.Ingredients.V1.Commands.CreateIngredient;

public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, string>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;
    public CreateIngredientCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task<string> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        var checkFoodCodeFilter = Builders<Ingredient>.Filter.Eq(x => x.Code, request.Code);
        var checkFoodCodeFilter2 = Builders<Ingredient>.Filter.Eq(x => x.IsDelete, false);

        var duplicate = await _uow.GenericRepository<Ingredient>()
            .GetSingleDocumentByFilterAsync(checkFoodCodeFilter & checkFoodCodeFilter2, cancellationToken);

        if (duplicate != null)
            throw new AppException("Duplicate data");            
        
        var nutrientValue = request.NutrientValue.Select(x => new NotNegativeForDecimalTypes(x)).ToList();
        var ingredient = new Ingredient
        {
            Code = request.Code,
            DefaultMeasureUnitId = ObjectId.Parse(request.DefaultMeasureUnitId),
            NutrientValue = nutrientValue,
            Translation = request.Name.ToEntity<IngredientTranslation>(),
            Tag = request.Tag,
            IsAllergy = request.IsAllergy,
            TagAr = request.TagAr,
            TagEn = request.TagEn,
            ThumbName = _fileService.AddImage(request.ThumbUri,"IngredientImages",request.Code),
        };
         foreach (var requestMeasureUnitId in request.MeasureUnitIds)
         {
            ingredient.MeasureUnitIds.Add(ObjectId.Parse(requestMeasureUnitId));
         }

        ingredient.MeasureUnitIds.Add(ObjectId.Parse(DefaultMeasureUnitConstants.GramsId));
        ingredient.MeasureUnitIds.Add(ObjectId.Parse(DefaultMeasureUnitConstants.OuncesId));
        ingredient.MeasureUnitIds.Add(ObjectId.Parse(DefaultMeasureUnitConstants.PoundsId));
        ingredient.MeasureUnitIds = ingredient.MeasureUnitIds.Distinct().ToList();
        await _uow.GenericRepository<Ingredient>().InsertOneAsync(ingredient, null, cancellationToken);
        return ingredient.Id;
    }
}