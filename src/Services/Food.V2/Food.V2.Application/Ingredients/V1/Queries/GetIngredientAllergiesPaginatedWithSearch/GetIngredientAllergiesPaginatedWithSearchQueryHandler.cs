namespace Food.V2.Application.Ingredients.V1.Queries.GetIngredientAllergiesPaginatedWithSearch;

public class GetIngredientAllergiesPaginatedWithSearchQueryHandler : IRequestHandler<
    GetIngredientAllergiesPaginatedWithSearchQuery, PaginationResult<IngredientDto>>
{
    private readonly IUnitOfWork _uow;

    public GetIngredientAllergiesPaginatedWithSearchQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PaginationResult<IngredientDto>> Handle(GetIngredientAllergiesPaginatedWithSearchQuery request,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.Name))
        {
            var filter = Builders<Ingredient>.Filter.Eq(x => x.Translation.Persian, request.Name);
            var filter2 = Builders<Ingredient>.Filter.Eq(x => x.IsAllergy, true);
            var data = await _uow.GenericRepository<Ingredient>()
                .GetListOfDocumentsByFilterAsync(filter & filter2, cancellationToken);

            var dtoResult = data.ToDto<IngredientDto>().ToList();

            return PaginationResult<IngredientDto>.CreatePaginationResult(request.PageIndex, request.PageSize,
                dtoResult.Count, dtoResult);
        }

        var allergyFilter = Builders<Ingredient>.Filter.Eq(x => x.IsAllergy, true);
        var dataWithFilter = await _uow.GenericRepository<Ingredient>()
            .GetListOfDocumentsByFilterAsync(allergyFilter, cancellationToken);

        var finalResult = dataWithFilter.ToDto<IngredientDto>().ToList();

        return PaginationResult<IngredientDto>.CreatePaginationResult(request.PageIndex, request.PageSize,
            finalResult.Count, finalResult);
        
    }
}