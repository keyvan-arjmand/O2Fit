namespace Identity.V2.Application.Permissions.V1.Commands.UpdatePermission;

public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdatePermissionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var categoryPermission =
            await _uow.GenericRepository<CategoryPermission>().GetByIdAsync(request.PermissionCategoryId, cancellationToken).ConfigureAwait(false);
        var index = categoryPermission.Permissions.FindIndex(x => x.Id == request.PermissionId);

        var filter =
            Builders<CategoryPermission>.Filter.ElemMatch(x => x.Permissions,
                permission => permission.Id == request.PermissionId);

        var update = Builders<CategoryPermission>.Update.Set(x => x.Permissions[index].Name,
            request.PermissionName);

        await _uow.GenericRepository<CategoryPermission>()
            .UpdateOneAsync(filter, categoryPermission, update, null, cancellationToken).ConfigureAwait(false);
    }
}