namespace Identity.V2.Application.PermissionCategories.V1.Commands.UpdateCategoryPermission;

public record UpdateCategoryPermissionCommand(string Id, string Name) : IRequest
{
    
}