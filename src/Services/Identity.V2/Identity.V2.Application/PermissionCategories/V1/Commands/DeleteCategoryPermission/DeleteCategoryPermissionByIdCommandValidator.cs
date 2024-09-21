namespace Identity.V2.Application.PermissionCategories.V1.Commands.DeleteCategoryPermission;

public class DeleteCategoryPermissionByIdCommandValidator : AbstractValidator<DeleteCategoryPermissionByIdCommand>
{
    private readonly IUnitOfWork _uow;
    public DeleteCategoryPermissionByIdCommandValidator(IUnitOfWork uow)
    {
        _uow = uow;

        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null")
            .MustAsync(IsNotExistsByIdAsync).WithMessage("The specified Id not exists.");
    }
    private async Task<bool> IsNotExistsByIdAsync(string id, CancellationToken cancellationToken)
    {
        var permissionCategory = await _uow.GenericRepository<CategoryPermission>().GetByIdAsync(id, cancellationToken)
            .ConfigureAwait(false);
        return permissionCategory != null;
    }
}