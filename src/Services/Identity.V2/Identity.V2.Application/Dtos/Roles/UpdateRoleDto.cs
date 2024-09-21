namespace Identity.V2.Application.Dtos.Roles;

public class UpdateRoleDto
{
    public string OldRoleName { get; set; } = default!;
    public string RoleName { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
}