using MongoDB.Bson;
using Ticket.Application.Common.Interfaces.Persistence.UoW;
using Ticket.Domain.Aggregates.TicketAggregate;
using Ticket.Domain.Enums;

namespace Ticket.Application.Tickets.V1.Commands.InsertUserTicket;

public class InsertUserTicketCommandHandler : IRequestHandler<InsertUserTicketCommand>
{
    private readonly IUnitOfWork _work;

    public InsertUserTicketCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(InsertUserTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = new Domain.Aggregates.TicketAggregate.Ticket
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Classification = request.Classification,
            Title = request.Title,
            IsEnd = false,
            TicketMessages = new List<TicketMessage>()
        };
        ticket.TicketMessages.Add(new TicketMessage
        {
            UserType = request.UserType,
            Message = request.Message,
            IsAdminRead = request.UserType == UserType.Admin,
            IsUserRead = request.UserType != UserType.Admin,
            TicketId = ticket.Id.StringToObjectId(),
        });
        await _work.GenericRepository<Domain.Aggregates.TicketAggregate.Ticket>()
            .InsertOneAsync(ticket, null, cancellationToken);
    }
}