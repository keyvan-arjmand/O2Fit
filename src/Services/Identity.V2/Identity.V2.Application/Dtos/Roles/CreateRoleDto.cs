namespace Identity.V2.Application.Dtos.Roles;

public class CreateRoleDto
{
    public string RoleName { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
}