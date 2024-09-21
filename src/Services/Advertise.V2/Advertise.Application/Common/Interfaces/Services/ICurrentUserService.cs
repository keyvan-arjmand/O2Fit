namespace Advertise.Application.Common.Interfaces.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Username { get; }
    string? Fullname { get; }
    string? Language { get; }
    string? StateId { get; }

    public void InsertAuditLog(AggregateRoot aggregateRoot);
    public void InsertAuditLog(IEnumerable<AggregateRoot> aggregateRoots);

    public void UpdateAuditLog(AggregateRoot aggregateRoot);
}