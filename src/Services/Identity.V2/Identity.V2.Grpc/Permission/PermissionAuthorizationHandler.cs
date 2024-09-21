using Common.Constants.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Identity.V2.Grpc.Permission;

public class PermissionAuthorizationHandler: AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        //var user = context.User;
        //var userIsAnonymous = user.Identity == null || !user.Identities.Any(x => x.IsAuthenticated);
        //if (userIsAnonymous)
        //{ 
        //   context.Succeed(requirement);
        //}
        var permissions = context.User.Claims.Where(x => x.Type == O2fitIdentityConstants.PermissionToLower &&
                                                          x.Value == requirement.Permission);
        if (permissions.Any())
        {
            context.Succeed(requirement);
            return Task.CompletedTask;

        }

        context.Fail();
        return Task.CompletedTask;

    }
}