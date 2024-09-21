namespace Identity.V2.Application.PermissionCategories.V1.Commands.DeleteCategoryPermission;

public record DeleteCategoryPermissionByIdCommand(string Id) : IRequest;