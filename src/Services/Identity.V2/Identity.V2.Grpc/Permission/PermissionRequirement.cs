using Microsoft.AspNetCore.Authorization;

namespace Identity.V2.Grpc.Permission;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; private set; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}