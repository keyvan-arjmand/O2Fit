namespace Identity.V2.Application.PermissionCategories.V1.Queries.GetAllCategoryPermissionsPaginated;

public class GetAllPermissionCategoriesPaginatedQueryHandler : IRequestHandler<GetAllPermissionCategoriesPaginatedQuery, PaginationResult<CategoryPermissionPaginatedDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllPermissionCategoriesPaginatedQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PaginationResult<CategoryPermissionPaginatedDto>> Handle(GetAllPermissionCategoriesPaginatedQuery request, CancellationToken cancellationToken)
    {
        var data = await _uow.GenericRepository<CategoryPermission>()
            .GetAllPaginationAsync(request.PageIndex, request.PageSize, cancellationToken).ConfigureAwait(false);
        var dto = PaginationResult<CategoryPermissionPaginatedDto>.CreatePaginationResult(
            data.PageIndex, data.PageSize, data.Count, data.Data.ToDto<CategoryPermissionPaginatedDto>().ToList()
        );
        return dto;

    }
}