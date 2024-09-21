namespace Identity.V2.Application.Permissions.V1.Commands.CreatePermission;

public record CreatePermissionCommand(string PermissionCategoryId,string PermissionName) : IRequest<string>;