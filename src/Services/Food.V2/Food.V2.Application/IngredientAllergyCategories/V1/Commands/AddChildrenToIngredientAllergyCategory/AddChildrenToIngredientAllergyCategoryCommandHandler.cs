using Food.V2.Application.Common.ApiResult;

namespace Food.V2.Application.IngredientAllergyCategories.V1.Commands.AddChildrenToIngredientAllergyCategory;

public class
    AddChildrenToIngredientAllergyCategoryCommandHandler : IRequestHandler<
        AddChildrenToIngredientAllergyCategoryCommand>
{
    private readonly IUnitOfWork _uow;

    public AddChildrenToIngredientAllergyCategoryCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(AddChildrenToIngredientAllergyCategoryCommand request, CancellationToken cancellationToken)
    {
        //checking

        #region Checking

        var findRootFilter = Builders<Ingredient>.Filter.Eq(x => x.Id, request.RootId);
        var findRootFilter2 = Builders<Ingredient>.Filter.Eq(x => x.IsAllergy, true);
        var findRootFilter3 = Builders<Ingredient>.Filter.Eq(x => x.IsDelete, false);

        var findRoot = await _uow.GenericRepository<Ingredient>()
            .GetSingleDocumentByFilterAsync(findRootFilter & findRootFilter2 & findRootFilter3, cancellationToken);
        if (findRoot == null)
            throw new NotFoundException(nameof(Ingredient), request.RootId);

        var findChildFilter = Builders<Ingredient>.Filter.Eq(x => x.Id, request.ChildId);
        var findChildFilter2 = Builders<Ingredient>.Filter.Eq(x => x.IsAllergy, true);
        var findChildFilter3 = Builders<Ingredient>.Filter.Eq(x => x.IsDelete, false);

        var findChild = await _uow.GenericRepository<Ingredient>()
            .GetSingleDocumentByFilterAsync(findChildFilter & findChildFilter2 & findChildFilter3, cancellationToken);
        if (findChild == null)
            throw new NotFoundException(nameof(Ingredient), request.ChildId);

        //check duplicate
        var findChildInIngredientAllergyCategoryFilter =
            Builders<IngredientAllergyCategory>.Filter.AnyEq(x => x.IngredientAllergiesIds, ObjectId.Parse(request.ChildId));
        var findByChild = await _uow.GenericRepository<IngredientAllergyCategory>()
            .GetSingleDocumentByFilterAsync(findChildInIngredientAllergyCategoryFilter, cancellationToken);
        if (findByChild != null)
            throw new AppException("Duplicate data");        
        #endregion

        var ingredientAllergyFilter1 =
            Builders<IngredientAllergyCategory>.Filter.Eq(x => x.IngredientId, ObjectId.Parse(request.RootId));
        var ingredientAllergyFilter2 =
            Builders<IngredientAllergyCategory>.Filter.Eq(x => x.IsDelete, false);


        var ingredientAllergy = await _uow.GenericRepository<IngredientAllergyCategory>()
            .GetSingleDocumentByFilterAsync(ingredientAllergyFilter1 & ingredientAllergyFilter2, cancellationToken);
        if (ingredientAllergy == null)
            throw new NotFoundException(nameof(IngredientAllergyCategory), request.RootId);

        var filter = Builders<IngredientAllergyCategory>.Filter.Eq(x => x.IngredientId, ObjectId.Parse(request.RootId));
        var update =
            Builders<IngredientAllergyCategory>.Update.Push(x => x.IngredientAllergiesIds,
                ObjectId.Parse(request.ChildId));

        await _uow.GenericRepository<IngredientAllergyCategory>()
            .UpdateOneAsync(filter, ingredientAllergy, update, null, cancellationToken);
    }
}