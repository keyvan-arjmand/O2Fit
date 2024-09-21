namespace Food.V2.Application.RecipeCategories.V1.Queries.GetRecipeCategoryPaginated;

public class GetRecipeCategoryPaginatedQueryHandler : IRequestHandler<GetRecipeCategoryPaginatedQuery, PaginationResult<RecipeCategoryDto>>
{
    private readonly IUnitOfWork _uow;

    public GetRecipeCategoryPaginatedQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PaginationResult<RecipeCategoryDto>> Handle(GetRecipeCategoryPaginatedQuery request, CancellationToken cancellationToken)
    {
        var data = await _uow.GenericRepository<RecipeCategory>()
            .GetAllPaginationAsync(request.PageIndex, request.PageSize, cancellationToken);

        var dtoResult = data.Data.ToDto<RecipeCategoryDto>().ToList();
        return PaginationResult<RecipeCategoryDto>.CreatePaginationResult(request.PageIndex, request.PageSize,
            dtoResult.Count, dtoResult);
    }
}