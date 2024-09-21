namespace Advertise.Api.Permission;

public class HasPermissionAttribute : AuthorizeAttribute
    
{
    public HasPermissionAttribute(string permission): base(policy: permission)
    {
        AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
    }
}