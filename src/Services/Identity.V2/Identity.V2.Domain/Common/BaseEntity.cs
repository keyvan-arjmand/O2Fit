namespace Identity.V2.Domain.Common;

[BsonIgnoreExtraElements]
public abstract class BaseEntity
{
    //[BsonRepresentation(BsonType.ObjectId)]
    [BsonId]
    public string Id { get; set; } = string.Empty;
    [BsonElement("isDelete")]
    public bool IsDelete { get; set; } = false;
}