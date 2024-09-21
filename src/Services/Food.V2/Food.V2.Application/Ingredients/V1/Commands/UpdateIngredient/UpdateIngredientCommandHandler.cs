namespace Food.V2.Application.Ingredients.V1.Commands.UpdateIngredient;

public class UpdateIngredientCommandHandler: IRequestHandler<UpdateIngredientCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;
    public UpdateIngredientCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
    {
        var ingredient = await _uow.GenericRepository<Ingredient>().GetByIdAsync(request.Id, cancellationToken);
        if (ingredient == null)
            throw new NotFoundException(nameof(Ingredient), request.Id);

        if (!string.IsNullOrEmpty(ingredient.ThumbName))
        {
            _fileService.RemoveImage(ingredient.ThumbName,"IngredientImages");            
        }
        
        ingredient.NutrientValue = request.NutrientValue.Select(x => new NotNegativeForDecimalTypes(x)).ToList();
        ingredient.MeasureUnitIds = new();
        ingredient.Code = request.Code;
        ingredient.Tag = request.Tag;
        ingredient.TagEn = request.TagEn;
        ingredient.TagAr = request.TagAr;
        ingredient.Translation.Persian = request.Name.Persian;
        ingredient.Translation.Arabic = request.Name.Arabic;
        ingredient.Translation.English = request.Name.English;
        ingredient.IsAllergy = request.IsAllergy;
        ingredient.ThumbName = _fileService.AddImage(request.ThumbUri,"IngredientImages",request.Code);
        ingredient.DefaultMeasureUnitId = ObjectId.Parse(request.DefaultMeasureUnitId);

        foreach (var requestMeasureUnitId in request.MeasureUnitIds)
        {
            ingredient.MeasureUnitIds.Add(ObjectId.Parse(requestMeasureUnitId));
        }
        ingredient.MeasureUnitIds.Add(ObjectId.Parse(DefaultMeasureUnitConstants.GramsId));
        ingredient.MeasureUnitIds.Add(ObjectId.Parse(DefaultMeasureUnitConstants.OuncesId));
        ingredient.MeasureUnitIds.Add(ObjectId.Parse(DefaultMeasureUnitConstants.PoundsId));
        ingredient.MeasureUnitIds = ingredient.MeasureUnitIds.Distinct().ToList();
        
        await _uow.GenericRepository<Ingredient>().UpdateOneAsync(x => x.Id == request.Id, ingredient,
            new Expression<Func<Ingredient, object>>[]
            {
                x => x.NutrientValue,
                x => x.MeasureUnitIds,
                x => x.Code,
                x => x.Tag,
                x=>x.TagEn,
                x=>x.TagAr,
                x => x.Translation.Persian,
                x => x.Translation.Arabic,
                x => x.Translation.English,
                x => x.IsAllergy,
                x => x.ThumbName,
                x => x.DefaultMeasureUnitId
            },null,cancellationToken);
    }
}