using Microsoft.AspNetCore.Authorization;
using OpenIddict.Validation.AspNetCore;

namespace Identity.V2.Grpc.Permission;

public class HasPermissionAttribute : AuthorizeAttribute
    
{
    public HasPermissionAttribute(string permission): base(policy: permission)
    {
        AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
    }
}