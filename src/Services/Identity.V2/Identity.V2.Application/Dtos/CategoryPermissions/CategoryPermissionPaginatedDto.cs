namespace Identity.V2.Application.Dtos.CategoryPermissions;

public class CategoryPermissionPaginatedDto : IDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
}