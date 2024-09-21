namespace Identity.V2.Application.Roles.V1.Commands.AddSinglePermissionToRole;

public class AddSinglePermissionToRoleCommandValidator: AbstractValidator<AddSinglePermissionToRoleCommand>
{
    public AddSinglePermissionToRoleCommandValidator()
    {
        RuleFor(x=>x.PermissionName).NotEmpty().WithMessage("PermissionName can not be empty")
            .NotNull().WithMessage("PermissionName can not be null");
            
       RuleFor(x=>x.RoleId).NotEmpty().WithMessage("Role can not be empty")
            .NotNull().WithMessage("Role can not be null");
    }
}