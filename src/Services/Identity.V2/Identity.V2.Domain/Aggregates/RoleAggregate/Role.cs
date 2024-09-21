using Identity.V2.Domain.Common.Role;

namespace Identity.V2.Domain.Aggregates.RoleAggregate;

public class Role : RoleAggregateRoot
{
    public Role()
    {
        
    }
    public Role(string displayName) : base()
    {
        DisplayName = displayName;
    }
    public Role(string roleName, string displayName) : base(roleName)
    {
        DisplayName = displayName;
        Permissions = new List<Permission>();
    }

    public string DisplayName { get; set; }

    public List<Permission> Permissions { get; set; } = default!;

}