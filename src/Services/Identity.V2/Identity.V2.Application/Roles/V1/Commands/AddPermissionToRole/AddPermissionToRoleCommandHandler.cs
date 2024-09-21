using Permission = Identity.V2.Domain.Aggregates.RoleAggregate.Permission;

namespace Identity.V2.Application.Roles.V1.Commands.AddPermissionToRole;

public class AddPermissionToRoleCommandHandler : IRequestHandler<AddPermissionToRoleCommand>
{
    private readonly IUnitOfWork _uow;

    public AddPermissionToRoleCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(AddPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        var roleFilter = Builders<Role>.Filter.Eq(x => x.Id, request.Role.Id);
        var roleUpdate = Builders<Role>.Update.Set(x => x.Permissions, new List<Permission>());
        await _uow.RoleGenericRepository<Role>()
            .UpdateOneAsync(roleFilter, request.Role, roleUpdate, null, cancellationToken).ConfigureAwait(false);

        foreach (var claimName in request.SelectedPermissionNames)
        {

                var filter = Builders<Role>.Filter.Eq(x => x.Id, request.Role.Id);
                var claimPermission = new Permission(claimName);
                var update = Builders<Role>.Update.Push(x => x.Permissions, claimPermission);

                await _uow.RoleGenericRepository<Role>()
                    .UpdateOneAsync(filter, request.Role, update, null, cancellationToken).ConfigureAwait(false);
        }
    }
}