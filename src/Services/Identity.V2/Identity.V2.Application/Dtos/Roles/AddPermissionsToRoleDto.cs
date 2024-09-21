namespace Identity.V2.Application.Dtos.Roles;

public class AddPermissionsToRoleDto
{
    public string RoleId { get; set; } = string.Empty;
    public List<string> Permissions { get; set; } = null!;
}