using Ticket.Domain.Aggregates.MessageAggregate;

namespace Ticket.Infrastructure.Persistence.Repositories;

public class MessageRepository:GenericRepository<Message>,IMessageRepository<Message>
{
    public MessageRepository(IMediator mediator, IConfiguration configuration, ICurrentUserService currentUserService) : base(mediator, configuration, currentUserService)
    {
    }

    public Task<Message?> GetByParentIdAsync(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}