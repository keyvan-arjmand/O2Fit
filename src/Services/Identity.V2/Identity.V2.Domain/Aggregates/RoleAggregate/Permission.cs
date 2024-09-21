namespace Identity.V2.Domain.Aggregates.RoleAggregate;

public class Permission : BaseEntity
{
    public Permission()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public Permission(string permissionName)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Name = permissionName;
    }
    public string Name { get; set; }
}