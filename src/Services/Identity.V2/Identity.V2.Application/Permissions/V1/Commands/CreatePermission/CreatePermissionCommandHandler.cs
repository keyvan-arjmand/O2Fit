using Permission = Identity.V2.Domain.Aggregates.PermissionCategoryAggregate.Permission;

namespace Identity.V2.Application.Permissions.V1.Commands.CreatePermission;

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, string>
{
    private readonly IUnitOfWork _uow;

    public CreatePermissionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<string> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permissionCategory = await _uow.GenericRepository<CategoryPermission>()
            .GetByIdAsync(request.PermissionCategoryId, cancellationToken).ConfigureAwait(false);
        var permission = new Permission(request.PermissionName);

        var filter = Builders<CategoryPermission>.Filter.Eq(x => x.Id, request.PermissionCategoryId);
        var update = Builders<CategoryPermission>.Update.Push<Permission>(x => x.Permissions, permission);

        await _uow.GenericRepository<CategoryPermission>()
            .UpdateOneAsync(filter, permissionCategory, update, null, cancellationToken).ConfigureAwait(false);
        return permission.Id;
    }
}