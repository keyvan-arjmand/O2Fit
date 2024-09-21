namespace Identity.V2.Application.PermissionCategories.V1.Commands.DeleteCategoryPermission;

public class DeleteCategoryPermissionByIdCommandHandler : IRequestHandler<DeleteCategoryPermissionByIdCommand>
{
    private readonly IUnitOfWork _uow;

    public DeleteCategoryPermissionByIdCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(DeleteCategoryPermissionByIdCommand request, CancellationToken cancellationToken)
    {
        var permissionCategory = await _uow.GenericRepository<CategoryPermission>()
            .GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
        await _uow.GenericRepository<CategoryPermission>().DeleteOneAsync(x => x.Id == request.Id,
            permissionCategory,null,cancellationToken).ConfigureAwait(false);
    }
}