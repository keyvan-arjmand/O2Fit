namespace Identity.V2.Application.PermissionCategories.V1.Queries.GetCategoryPermissionByIdWithPermissions;

public class GetPermissionCategoryByIdWithPermissionQueryHandler : IRequestHandler<GetPermissionCategoryByIdWithPermissionQuery, CategoryPermissionWithPermissionsDto>
{
    private readonly IUnitOfWork _uow;

    public GetPermissionCategoryByIdWithPermissionQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<CategoryPermissionWithPermissionsDto> Handle(GetPermissionCategoryByIdWithPermissionQuery request, CancellationToken cancellationToken)
    {
        var permissionCategory = await _uow.GenericRepository<CategoryPermission>()
            .GetByIdAsync(request.PermissionCategoryId, cancellationToken).ConfigureAwait(false);

        if (permissionCategory is null)
            throw new NotFoundException(nameof(CategoryPermission), request.PermissionCategoryId);

        return permissionCategory.ToDto<CategoryPermissionWithPermissionsDto>();
    }
}