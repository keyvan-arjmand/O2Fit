namespace Identity.V2.Domain.Aggregates.PermissionCategoryAggregate;

public class Permission : BaseEntity
{
    public Permission()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    public Permission(string name)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Name = name;
    }
    public string Name { get; set; }
}