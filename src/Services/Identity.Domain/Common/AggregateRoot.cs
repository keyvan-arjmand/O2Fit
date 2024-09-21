using System.ComponentModel.DataAnnotations.Schema;
using EventBus.Messages.Events;

namespace Identity.Domain.Common;
public interface IAggregateRoot
{
    
}

public class AggregateRoot : BaseAuditableEntity,IAggregateRoot
{
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