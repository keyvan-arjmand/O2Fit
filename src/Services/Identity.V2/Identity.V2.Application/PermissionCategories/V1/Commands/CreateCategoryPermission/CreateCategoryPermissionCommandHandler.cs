using Permission = Identity.V2.Domain.Aggregates.PermissionCategoryAggregate.Permission;

namespace Identity.V2.Application.PermissionCategories.V1.Commands.CreateCategoryPermission;

public class CreateCategoryPermissionCommandHandler : IRequestHandler<CreateCategoryPermissionCommand, string>
{
    private readonly IUnitOfWork _uow;

    public CreateCategoryPermissionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<string> Handle(CreateCategoryPermissionCommand request, CancellationToken cancellationToken)
    {
        var permissionCategory = new CategoryPermission(request.Name)
        {
            Permissions = new List<Permission>()
        };
        await _uow.GenericRepository<CategoryPermission>()
            .InsertOneAsync(permissionCategory, null, cancellationToken).ConfigureAwait(false);

        return permissionCategory.Id;

    }
}