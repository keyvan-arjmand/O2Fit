using System.Security.Claims;

namespace Identity.V2.Application.Common.Extensions;

public static class ClaimExtensions
{
    public static async Task AddPermissionClaimAsync(this RoleManager<Role> roleManager, Role role, string permission)
    {
        var allClaims = await roleManager.GetClaimsAsync(role).ConfigureAwait(false);
        if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
        {
            await roleManager.AddClaimAsync(role, new Claim("Permission", permission)).ConfigureAwait(false);
        }
    }
}