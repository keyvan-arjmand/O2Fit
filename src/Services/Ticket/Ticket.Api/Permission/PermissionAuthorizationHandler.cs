namespace Ticket.Api.Permission;

public class PermissionAuthorizationHandler: AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
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