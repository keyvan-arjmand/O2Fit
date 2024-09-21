using Ticket.Application.Dtos;

namespace Ticket.Application.Tickets.V1.Queries.GetTicketById;

public record GetTicketByIdQuery(string Id) : IRequest<UserTicketDto>;
