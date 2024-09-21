namespace Identity.V2.Application.PermissionCategories.V1.Commands.CreateCategoryPermission;

public record CreateCategoryPermissionCommand(string Name) : IRequest<string>;