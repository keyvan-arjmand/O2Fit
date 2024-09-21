using Food.V2.Application.Common.ApiResult;

namespace Food.V2.Application.IngredientAllergyCategories.V1.Commands.CreateRootIngredientAllergy;

public class CreateRootIngredientAllergyCommandHandler : IRequestHandler<CreateRootIngredientAllergyCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateRootIngredientAllergyCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(CreateRootIngredientAllergyCommand request, CancellationToken cancellationToken)
    {
        var findIngredientFilter = Builders<Ingredient>.Filter.Eq(x => x.Id, request.RootId);
        var findIngredientFilter2 = Builders<Ingredient>.Filter.Eq(x => x.IsAllergy, true);
        var findIngredientFilter3 = Builders<Ingredient>.Filter.Eq(x => x.IsDelete, false);

        var findIngredient = await _uow.GenericRepository<Ingredient>()
            .GetSingleDocumentByFilterAsync(findIngredientFilter & findIngredientFilter2 & findIngredientFilter3, cancellationToken);
        if (findIngredient == null)
            throw new NotFoundException(nameof(Ingredient), request.RootId);

        var filter = Builders<IngredientAllergyCategory>.Filter.Eq(x => x.IngredientId, ObjectId.Parse(request.RootId));
        var filter2 = Builders<IngredientAllergyCategory>.Filter.Eq(x => x.IsDelete, false);

        var getIngredientAllergy = await _uow.GenericRepository<IngredientAllergyCategory>()
            .GetSingleDocumentByFilterAsync(filter & filter2, cancellationToken);
        if (getIngredientAllergy != null)
            throw new AppException("Duplicate data");
            
        var ingredientAllergy = new IngredientAllergyCategory
        {
            IngredientId = ObjectId.Parse(request.RootId),
            IngredientAllergiesIds = new()
        };
        await _uow.GenericRepository<IngredientAllergyCategory>()
            .InsertOneAsync(ingredientAllergy, null, cancellationToken);
    }
}