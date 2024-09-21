using Ticket.Domain.Aggregates.MessageAggregate;

namespace Ticket.Application.Common.Interfaces.Persistence.Repositories;

public interface IMessageRepository<T> where T : Message
{
   Task<T?> GetByParentIdAsync(string id, CancellationToken cancellationToken = default);
}