namespace Identity.V2.Application.Dtos.CategoryPermissions;

public class CategoryPermissionWithPermissionsForTreeViewDto : IDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<PermissionsForTreeViewDto> Permissions { get; set; } = default!;
}