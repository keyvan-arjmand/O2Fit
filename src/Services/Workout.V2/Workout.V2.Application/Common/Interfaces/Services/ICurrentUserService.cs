namespace Workout.V2.Application.Common.Interfaces.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Username { get; }
    public void InsertAuditLog(AggregateRoot aggregateRoot);
    public void InsertAuditLog(IEnumerable<AggregateRoot> aggregateRoots);

    public void UpdateAuditLog(AggregateRoot aggregateRoot);
}