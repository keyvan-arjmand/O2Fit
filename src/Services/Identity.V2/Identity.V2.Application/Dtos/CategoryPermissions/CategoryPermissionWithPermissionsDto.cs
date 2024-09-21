namespace Identity.V2.Application.Dtos.CategoryPermissions;

public class CategoryPermissionWithPermissionsDto : IDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<PermissionsDto> Permissions { get; set; } = default!;
}