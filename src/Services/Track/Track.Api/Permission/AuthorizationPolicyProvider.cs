namespace Track.Api.Permission;

public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    public override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!policyName.StartsWith(PermissionAuthorizeAttribute.PolicyPerfix, StringComparison.OrdinalIgnoreCase))
        {
            return base.GetPolicyAsync(policyName);
        }

        var permissionNames = policyName.Substring(PermissionAuthorizeAttribute.PolicyPerfix.Length).Split(",");
        var policy = new AuthorizationPolicyBuilder()
            .RequireClaim(PermissionsConstants.Permissions, permissionNames)
            .Build();
        return Task.FromResult(policy)!;
    }
}