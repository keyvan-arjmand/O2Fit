using Permission = Identity.V2.Domain.Aggregates.PermissionCategoryAggregate.Permission;

namespace Identity.V2.Application.Permissions.V1.Commands.DeletePermission;

public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand>
{
    private readonly IUnitOfWork _uow;

    public DeletePermissionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        var permissionCategory = await _uow.GenericRepository<CategoryPermission>()
            .GetByIdAsync(request.PermissionCategoryId, cancellationToken).ConfigureAwait(false);

        var filter = Builders<CategoryPermission>.Filter.ElemMatch(x => x.Permissions,
            permission => permission.Id == request.PermissionId);

        var result = await _uow.GenericRepository<CategoryPermission>().GetSingleDocumentByFilterAsync(filter, cancellationToken).ConfigureAwait(false);
        
        if (result is null)
            throw new NotFoundException(nameof(Permission), nameof(request.PermissionId));

        var update = Builders<CategoryPermission>.Update.PullFilter(x => x.Permissions,
            permission => permission.Id == request.PermissionId);

        await _uow.GenericRepository<CategoryPermission>()
            .UpdateOneAsync(filter, permissionCategory, update, null, cancellationToken).ConfigureAwait(false);

    }
}