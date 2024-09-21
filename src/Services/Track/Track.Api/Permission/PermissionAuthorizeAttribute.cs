namespace Track.Api.Permission;

public class PermissionAuthorizeAttribute:AuthorizeAttribute
{
    internal const string PolicyPerfix = "Permission:";

    public PermissionAuthorizeAttribute(params string[] permissions)
    {
        Policy = $"{PolicyPerfix}{string.Join(",", permissions)}";
    }
}