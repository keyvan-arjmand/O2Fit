namespace Identity.V2.Domain.Common.Role;


public abstract class RoleBaseEntity : MongoRole
{
    protected RoleBaseEntity() : base(null)
    {

    }

    protected RoleBaseEntity(string roleName) : base(roleName)
    {
 
    }
    public bool IsDelete { get; set; }

    
}