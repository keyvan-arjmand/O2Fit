using Mongo.Migration.Documents;

namespace Track.Domain.Common;

[BsonIgnoreExtraElements]
public abstract class BaseEntity
{
    [BsonId] public string Id { get; set; } = string.Empty;
    public bool IsDelete { get; set; } = false;
    public DocumentVersion Version { get; set; } 
}