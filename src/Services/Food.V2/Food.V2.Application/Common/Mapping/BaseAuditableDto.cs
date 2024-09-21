namespace Food.V2.Application.Common.Mapping;

[BsonIgnoreExtraElements]
public class BaseAuditableDto : BaseDto
{
    public DateTime Created { get; set; }

    public ObjectId CreatedById { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public ObjectId LastModifiedById { get; set; }

    public string? LastModifiedBy { get; set; }
}