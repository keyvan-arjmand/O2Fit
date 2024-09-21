namespace Identity.V2.Domain.Aggregates.PermissionCategoryAggregate;

[BsonIgnoreExtraElements]
[CollectionLocation("categoryPermissions","default")]
public class CategoryPermission : AggregateRoot, IDocument
{
    public CategoryPermission()
    {
        
    }
    public CategoryPermission(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
    public List<Permission> Permissions { get; set; }
    [BsonElement("Version")]
    public DocumentVersion Version { get; set; }
}