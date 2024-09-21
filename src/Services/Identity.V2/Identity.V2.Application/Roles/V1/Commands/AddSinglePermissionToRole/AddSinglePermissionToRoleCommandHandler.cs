using Permission = Identity.V2.Domain.Aggregates.RoleAggregate.Permission;

namespace Identity.V2.Application.Roles.V1.Commands.AddSinglePermissionToRole;

public class AddSinglePermissionToRoleCommandHandler : IRequestHandler<AddSinglePermissionToRoleCommand>
{
    private readonly IUnitOfWork _uow;

    public AddSinglePermissionToRoleCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(AddSinglePermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _uow.RoleGenericRepository<Role>()
            .GetByIdAsync(ObjectId.Parse(request.RoleId), cancellationToken);
        if (role == null)
            throw new NotFoundException(nameof(Role), request.RoleId);
        
        var filter = Builders<Role>.Filter.Eq(x => x.Id, ObjectId.Parse(request.RoleId));
        var claimPermission = new Permission(request.PermissionName);
        var update = Builders<Role>.Update.Push(x => x.Permissions, claimPermission);

        await _uow.RoleGenericRepository<Role>()
            .UpdateOneAsync(filter, role, update, null, cancellationToken).ConfigureAwait(false);
    }
}