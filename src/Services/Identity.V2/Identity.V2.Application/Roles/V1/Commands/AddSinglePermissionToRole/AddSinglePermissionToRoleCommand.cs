namespace Identity.V2.Application.Roles.V1.Commands.AddSinglePermissionToRole;

public record AddSinglePermissionToRoleCommand(string RoleId, string PermissionName): IRequest;