namespace Track.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }

    public int CreatedById { get; set; } 

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public int LastModifiedById { get; set; } 

    public string? LastModifiedBy { get; set; }
}
