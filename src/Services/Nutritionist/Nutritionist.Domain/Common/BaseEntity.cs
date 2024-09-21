namespace Nutritionist.Domain.Common;

[BsonIgnoreExtraElements]
public abstract class BaseEntity
{
    [BsonId] public string Id { get; set; } = string.Empty;
    [BsonElement("isDelete")]
    public bool IsDelete { get; set; } = false;
}