using Ticket.Application.Common.Exceptions;
using Ticket.Application.Common.Interfaces.Persistence.UoW;
using Ticket.Domain.Aggregates.TicketAggregate;
using Ticket.Domain.Enums;

namespace Ticket.Application.Tickets.V1.Commands.PartialUpdateUserTicket;

public class PartialUpdateUserTicketCommandHandler : IRequestHandler<PartialUpdateUserTicketCommand>
{
    private readonly IUnitOfWork _work;

    public PartialUpdateUserTicketCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(PartialUpdateUserTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _work.GenericRepository<Domain.Aggregates.TicketAggregate.Ticket>()
            .GetByIdAsync(request.TicketId, cancellationToken);
        if (ticket == null) throw new NotFoundException($"Ticket not Found{request.TicketId}");
        ticket.TicketMessages.Add(new TicketMessage
        {
            UserType = request.TicketMessage.UserType,
            Message = request.TicketMessage.Message,
            IsAdminRead = request.TicketMessage.UserType == UserType.Admin,
            IsUserRead = request.TicketMessage.UserType != UserType.Admin,
            TicketId = ticket.Id.StringToObjectId(),
        });
        await _work.GenericRepository<Domain.Aggregates.TicketAggregate.Ticket>()
            .UpdateOneAsync(x => x.Id == ticket.Id, ticket,
                new Expression<Func<Domain.Aggregates.TicketAggregate.Ticket, object>>[]
                {
                    x => x.TicketMessages
                }, null, cancellationToken).ConfigureAwait(false);
    }
}