namespace Identity.V2.Application.Permissions.V1.Commands.UpdatePermission;

public record UpdatePermissionCommand(string PermissionCategoryId, string PermissionId, string PermissionName) : IRequest;