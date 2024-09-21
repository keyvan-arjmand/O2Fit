namespace Identity.V2.Application.Permissions.V1.Commands.UpdatePermission;

public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
{
    private readonly IUnitOfWork _uow;
    public UpdatePermissionCommandValidator(IUnitOfWork uow)
    {
        _uow = uow;

        RuleFor(x => x.PermissionName).NotEmpty().WithMessage("Claim name can not be empty")
            .NotNull().WithMessage("Claim name can not be null")
            .MinimumLength(5).WithMessage("Claim name must be longer than 5 characters")
            .MaximumLength(100).WithMessage("Claim name must be shorter than 100 characters")
            .MustAsync(BeUniqueClaimAsync).WithMessage("The specified claimName already exists.");

        RuleFor(x => x.PermissionId).NotEmpty().WithMessage("PermissionId can not be empty")
            .NotNull().WithMessage("PermissionId can not be null")
            .MustAsync(IsExistsAsync).WithMessage("Permission should be exists");

        RuleFor(x => x.PermissionCategoryId).NotEmpty().WithMessage("PermissionCategoryId can not be empty")
            .NotNull().WithMessage("PermissionCategoryId can not be null")
            .MustAsync(IsPermissionCategoryExistsAsync).WithMessage("Permission Category should be exists");
    }
    private async Task<bool> BeUniqueClaimAsync(string claimName, CancellationToken cancellationToken)
    {
        var filter = Builders<CategoryPermission>.Filter.ElemMatch(x => x.Permissions,
            permission => permission.Name == claimName);
        var result = await _uow.GenericRepository<CategoryPermission>().GetSingleDocumentByFilterAsync(filter, cancellationToken).ConfigureAwait(false);
        return result == null;
    }

    private async Task<bool> IsExistsAsync(string id, CancellationToken cancellationToken)
    {

        var filter = Builders<CategoryPermission>.Filter.ElemMatch(x => x.Permissions,
            permission => permission.Id == id);
        var result = await _uow.GenericRepository<CategoryPermission>().GetSingleDocumentByFilterAsync(filter, cancellationToken).ConfigureAwait(false);
        return result != null;
    }

    private async Task<bool> IsPermissionCategoryExistsAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _uow.GenericRepository<CategoryPermission>().GetByIdAsync(id, cancellationToken)
            .ConfigureAwait(false);
        return result != null;
    }
}