namespace Food.V2.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }

    public ObjectId CreatedById { get; set; } 

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public ObjectId LastModifiedById { get; set; } 

    public string? LastModifiedBy { get; set; }
}
