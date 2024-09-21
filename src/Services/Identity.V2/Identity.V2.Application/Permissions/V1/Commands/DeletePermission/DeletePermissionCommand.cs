namespace Identity.V2.Application.Permissions.V1.Commands.DeletePermission;

public record DeletePermissionCommand(string PermissionCategoryId, string PermissionId): IRequest;