namespace Identity.V2.Application.Roles.V1.Commands.AddPermissionToRole;

public class AddPermissionToRoleCommandValidator : AbstractValidator<AddPermissionToRoleCommand>
{
    public AddPermissionToRoleCommandValidator()
    {
        RuleFor(x => x.Role).NotEmpty().WithMessage("Role can not be empty")
            .NotNull().WithMessage("Role can not be null");

        RuleFor(x => x.SelectedPermissionNames)
            .NotEmpty().WithMessage("SelectedPermissionNames can not be empty")
            .NotNull().WithMessage("SelectedPermissionNames can not be null"); ;
        
    }
}