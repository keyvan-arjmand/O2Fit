namespace Food.V2.Application.IngredientAllergyCategories.V1.Commands.AddIngredientAllergyToRoot;

public class AddIngredientAllergyToRootCommandHandler : IRequestHandler<AddIngredientAllergyToRootCommand>
{
    private readonly IUnitOfWork _uow;

    public AddIngredientAllergyToRootCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(AddIngredientAllergyToRootCommand request, CancellationToken cancellationToken)
    {
        var findIngredientFilter = Builders<Ingredient>.Filter.Eq(x => x.Id, request.IngredientAllergyId);
        var findIngredientFilter2 = Builders<Ingredient>.Filter.Eq(x => x.IsAllergy, true);
        var findIngredientFilter3 = Builders<Ingredient>.Filter.Eq(x => x.IsDelete, false);

        var findIngredient = _uow.GenericRepository<Ingredient>()
            .GetSingleDocumentByFilterAsync(findIngredientFilter & findIngredientFilter2 & findIngredientFilter3, cancellationToken);
        if (findIngredient == null)
            throw new NotFoundException(nameof(Ingredient), request.IngredientAllergyId);
        
        var filter = Builders<IngredientAllergyCategory>.Filter.Eq(x => x.IngredientId, ObjectId.Parse(request.RootAllergyId));
        var filter2 = Builders<IngredientAllergyCategory>.Filter.Eq(x => x.IsDelete, false);

        var ingredientAllergyCategory = await _uow.GenericRepository<IngredientAllergyCategory>()
            .GetSingleDocumentByFilterAsync(filter & filter2, cancellationToken);

        if (ingredientAllergyCategory == null)
            throw new NotFoundException(nameof(IngredientAllergyCategory), request.RootAllergyId);

        var update =
            Builders<IngredientAllergyCategory>.Update.Push(x => x.IngredientAllergiesIds, ObjectId.Parse(request.IngredientAllergyId));

        await _uow.GenericRepository<IngredientAllergyCategory>()
            .UpdateOneAsync(filter, ingredientAllergyCategory, update, null, cancellationToken);
        
    }
}