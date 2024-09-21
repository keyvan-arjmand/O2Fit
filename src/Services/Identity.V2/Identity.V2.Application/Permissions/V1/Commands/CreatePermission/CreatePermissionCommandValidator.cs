namespace Identity.V2.Application.Permissions.V1.Commands.CreatePermission;

public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    private readonly IUnitOfWork _uow;
    public CreatePermissionCommandValidator(IUnitOfWork uow)
    {
        _uow = uow;

        RuleFor(x => x.PermissionName).NotEmpty().WithMessage("Permission name can not be empty")
            .NotNull().WithMessage("Permission name can not be null")
            .MinimumLength(5).WithMessage("Permission name must be longer than 5 characters")
            .MaximumLength(100).WithMessage("Permission name must be shorter than 100 characters")
            .MustAsync(BeUniqueClaimAsync).WithMessage("The specified Permission name already exists.");
    }

    private async Task<bool> BeUniqueClaimAsync(string permissionName, CancellationToken cancellationToken)
    {
        var filter = Builders<CategoryPermission>.Filter.ElemMatch(x => x.Permissions,
            item=>item.Name == permissionName);
        var result = await _uow.GenericRepository<CategoryPermission>().GetSingleDocumentByFilterAsync(filter, cancellationToken).ConfigureAwait(false);
        return result == null;
    }
}