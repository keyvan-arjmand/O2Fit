namespace Identity.V2.Application.Permissions.V1.Commands.DeletePermission;

public class DeletePermissionCommandValidator : AbstractValidator<DeletePermissionCommand>
{
    public DeletePermissionCommandValidator()
    {

        RuleFor(x => x.PermissionCategoryId).NotNull().WithMessage("Id can not be null")
            .NotEmpty().WithMessage("Id can not be empty");

    }
  
}