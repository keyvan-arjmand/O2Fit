namespace Food.V2.Application.IngredientAllergyCategories.V1.Queries.GetIngredientAllergyIds;

public class GetIngredientAllergyIdsQueryHandler : IRequestHandler<GetIngredientAllergyIdsQuery , string>
{
    private readonly IUnitOfWork _uow;

    public GetIngredientAllergyIdsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<string> Handle(GetIngredientAllergyIdsQuery request, CancellationToken cancellationToken)
    {
        var rootAllergyFilter = Builders<IngredientAllergyCategory>.Filter.Eq(x => x.IngredientId, ObjectId.Parse(request.IngredientId));
        
        var result = await _uow.GenericRepository<IngredientAllergyCategory>()
            .GetSingleDocumentByFilterAsync(rootAllergyFilter, cancellationToken);
        if (result != null)
        {
            return result.Id;
        }
        
        var ingredientAllergyFilter = Builders<IngredientAllergyCategory>.Filter.AnyEq(x => x.IngredientAllergiesIds,
            ObjectId.Parse(request.IngredientId));
        var ingredientAllergyResult = await _uow.GenericRepository<IngredientAllergyCategory>()
            .GetSingleDocumentByFilterAsync(ingredientAllergyFilter, cancellationToken);
        
        if (ingredientAllergyResult != null)
        {
            return ingredientAllergyResult.Id;
        }

        //if (result == null && ingredientAllergyResult == null)
        //    throw new NotFoundException(nameof(IngredientAllergyCategories), request.IngredientId);

        return string.Empty;
    }
}