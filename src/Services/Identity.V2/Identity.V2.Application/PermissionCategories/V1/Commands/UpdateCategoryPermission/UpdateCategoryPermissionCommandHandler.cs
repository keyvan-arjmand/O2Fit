namespace Identity.V2.Application.PermissionCategories.V1.Commands.UpdateCategoryPermission;

public class UpdateCategoryPermissionCommandHandler : IRequestHandler<UpdateCategoryPermissionCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateCategoryPermissionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateCategoryPermissionCommand request, CancellationToken cancellationToken)
    {
        var permissionCategory = await _uow.GenericRepository<CategoryPermission>()
            .GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
        var filter = Builders<CategoryPermission>.Filter.Eq(x => x.Id, request.Id);
        var update = Builders<CategoryPermission>.Update.Set(x => x.Name, request.Name);

        await _uow.GenericRepository<CategoryPermission>()
            .UpdateOneAsync(filter, permissionCategory, update, null, cancellationToken).ConfigureAwait(false);

    }
}