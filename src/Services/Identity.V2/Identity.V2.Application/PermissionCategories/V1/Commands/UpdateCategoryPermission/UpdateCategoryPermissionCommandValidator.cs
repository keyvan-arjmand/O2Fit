namespace Identity.V2.Application.PermissionCategories.V1.Commands.UpdateCategoryPermission;

public class UpdateCategoryPermissionCommandValidator : AbstractValidator<UpdateCategoryPermissionCommand>
{
    private readonly IUnitOfWork _uow;
    public UpdateCategoryPermissionCommandValidator(IUnitOfWork uow)
    {
        _uow = uow;

        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null")
            .MustAsync(IsNotExistsByIdAsync).WithMessage("The specified Id not exists.");

        RuleFor(x=>x.Name).NotEmpty().WithMessage("Name can not be empty")
            .NotNull().WithMessage("Name can not be null")
            .MinimumLength(5).WithMessage("Name must be longer than 5 characters")
            .MaximumLength(100).WithMessage("Name must be shorter than 100 characters")
            .MustAsync(BeUniqueNameAsync).WithMessage("The specified Name already exists.");
    }

    private async Task<bool> BeUniqueNameAsync(string name, CancellationToken cancellationToken)
    {
        var filter = Builders<CategoryPermission>.Filter.Eq(x => x.Name, name);
        var result = await _uow.GenericRepository<CategoryPermission>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken).ConfigureAwait(false);
        return result == null ? true : false;
    }


    private async Task<bool> IsNotExistsByIdAsync(string id, CancellationToken cancellationToken)
    {
        var permissionCategory = await _uow.GenericRepository<CategoryPermission>().GetByIdAsync(id, cancellationToken)
            .ConfigureAwait(false);
        return permissionCategory == null ? false : true ;
    }

}