namespace Identity.V2.ResourceServer.Permission;

public class HasPermissionAttribute : AuthorizeAttribute
    
{
    public HasPermissionAttribute(string permission): base(policy: permission)
    {
        AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
    }
}