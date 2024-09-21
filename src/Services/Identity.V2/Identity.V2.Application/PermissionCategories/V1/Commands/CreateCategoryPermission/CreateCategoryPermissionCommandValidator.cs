namespace Identity.V2.Application.PermissionCategories.V1.Commands.CreateCategoryPermission;

public class CreateCategoryPermissionCommandValidator : AbstractValidator<CreateCategoryPermissionCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateCategoryPermissionCommandValidator(IUnitOfWork uow)
    {
        _uow = uow;

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty")
            .NotNull().WithMessage("Name can not be null")
            //.MinimumLength(5).WithMessage("Name must be longer than 5 characters")
            .MaximumLength(100).WithMessage("Name must be shorter than 100 characters")
            .MustAsync(BeUniqueNameAsync).WithMessage("The specified Name already exists.");
    }

    private async Task<bool> BeUniqueNameAsync(string name, CancellationToken cancellationToken)
    {
        var filter = Builders<CategoryPermission>.Filter.Eq(x => x.Name, name);
        var result = await _uow.GenericRepository<CategoryPermission>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken).ConfigureAwait(false);
        return result == null;
    }
}