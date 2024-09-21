namespace Identity.V2.Domain.Common.Role;


public abstract class RoleAggregateRoot : RoleBaseEntity
{
    protected RoleAggregateRoot() : base(string.Empty)
    {
    }

    protected RoleAggregateRoot(string roleName) : base(roleName)
    {
    }
    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}