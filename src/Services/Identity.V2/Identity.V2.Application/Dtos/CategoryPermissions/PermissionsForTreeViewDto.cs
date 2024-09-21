namespace Identity.V2.Application.Dtos.CategoryPermissions;

public class PermissionsForTreeViewDto : IDto
{
    public string Name { get; set; } = default!;
    public bool IsSelected { get; set; }
}