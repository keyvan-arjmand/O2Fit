namespace Identity.V2.Application.Roles.V1.Commands.AddPermissionToRole;

public class AddPermissionToRoleCommand: IRequest
{
    public Role Role { get; set; } = null!;

    public List<string> SelectedPermissionNames
    {
        get;
        set;
    } = null!;
}