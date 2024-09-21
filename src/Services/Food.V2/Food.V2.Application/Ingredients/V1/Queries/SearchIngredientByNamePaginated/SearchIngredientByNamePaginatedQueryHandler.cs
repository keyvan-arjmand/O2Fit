namespace Food.V2.Application.Ingredients.V1.Queries.SearchIngredientByNamePaginated;

public class
    SearchIngredientByNamePaginatedQueryHandler : IRequestHandler<SearchIngredientByNamePaginatedQuery,
        PaginationResult<SearchIngredientByNameDto>>
{
    private readonly IUnitOfWork _uow;

    public SearchIngredientByNamePaginatedQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PaginationResult<SearchIngredientByNameDto>> Handle(SearchIngredientByNamePaginatedQuery request,
        CancellationToken cancellationToken)
    {
        List<Ingredient>? ingredient = null;
        switch (request.Language)
        {
            case "English":
            {
                var filter = Builders<Ingredient>.Filter.Where(x=>x.Translation.English.ToLower().Contains(request.Name.ToLower()));
                ingredient = await _uow.GenericRepository<Ingredient>()
                    .GetListOfDocumentsByFilterAsync(filter!, cancellationToken);
                if (ingredient == null)
                    throw new NotFoundException(nameof(Ingredient), request.Name);
                break;
            }
            case "Arabic":
            {
                var filter = Builders<Ingredient>.Filter.Where(x=>x.Translation.Arabic.ToLower().Contains(request.Name.ToLower()));
                ingredient = await _uow.GenericRepository<Ingredient>()
                    .GetListOfDocumentsByFilterAsync(filter!, cancellationToken);
                if (ingredient == null)
                    throw new NotFoundException(nameof(Ingredient), request.Name);
                break;
            }
            case "Persian":
            {
                var filter = Builders<Ingredient>.Filter.Where(x=>x.Translation.Persian.ToLower().Contains(request.Name.ToLower()));
                ingredient = await _uow.GenericRepository<Ingredient>()
                    .GetListOfDocumentsByFilterAsync(filter!, cancellationToken);
                if (ingredient == null)
                    throw new NotFoundException(nameof(Ingredient), request.Name);
                break;
            }
            default:
            {
                var filter = Builders<Ingredient>.Filter.Where(x=>x.Translation.Persian.ToLower().Contains(request.Name.ToLower()));
                ingredient = await _uow.GenericRepository<Ingredient>()
                    .GetListOfDocumentsByFilterAsync(filter!, cancellationToken);
                if (ingredient == null)
                    throw new NotFoundException(nameof(Ingredient), request.Name);
                break;
            }
        }

        var result = ingredient.ToDto<SearchIngredientByNameDto>().ToList();
        
        var paginatedResult =
            PaginationResult<SearchIngredientByNameDto>.CreatePaginationResult(request.PageIndex, request.PageSize,
                result.Count, result);

        foreach (var data in paginatedResult.Data)
        {
            switch (request.Language)
            {
                case "English":
                    data.Name = ingredient.Where(x => x.Id == data.Id).FirstOrDefault().Translation.English;
                    continue;
                case "Arabic":
                    data.Name = ingredient.Where(x => x.Id == data.Id).FirstOrDefault().Translation.Arabic;
                    continue;
                case "Persian":
                    data.Name = ingredient.Where(x => x.Id == data.Id).FirstOrDefault().Translation.Persian;
                    continue;
                default:
                    data.Name = ingredient.Where(x => x.Id == data.Id).FirstOrDefault().Translation.Persian;
                    continue;
            }
        }
        return paginatedResult;
    }
}