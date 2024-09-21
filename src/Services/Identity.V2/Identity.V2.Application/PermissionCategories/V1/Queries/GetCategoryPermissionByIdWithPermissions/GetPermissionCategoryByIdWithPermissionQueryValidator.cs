namespace Identity.V2.Application.PermissionCategories.V1.Queries.GetCategoryPermissionByIdWithPermissions;

public class GetPermissionCategoryByIdWithPermissionQueryValidator : AbstractValidator<GetPermissionCategoryByIdWithPermissionQuery>
{
    private readonly IUnitOfWork _uow;
    public GetPermissionCategoryByIdWithPermissionQueryValidator(IUnitOfWork uow)
    {
        _uow = uow;

        RuleFor(x => x.PermissionCategoryId)
            .NotNull().WithMessage("PermissionCategoryId can not be null")
            .NotEmpty().WithMessage("PermissionCategoryId can not be empty")
            .MustAsync(IsExistsAsync).WithMessage("Permission category does not exists");
    }

    private async Task<bool> IsExistsAsync(string permissionCategoryId, CancellationToken cancellationToken)
    {
        var permissionCategory = await _uow.GenericRepository<CategoryPermission>()
            .GetByIdAsync(permissionCategoryId, cancellationToken).ConfigureAwait(false);

        return permissionCategory != null;
    }
}