using Ticket.Application.Common.Exceptions;
using Ticket.Application.Common.Interfaces.Persistence.UoW;
using Ticket.Application.Common.Mapping;
using Ticket.Application.Dtos;

namespace Ticket.Application.Tickets.V1.Queries.GetTicketById;

public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, UserTicketDto>
{
    private readonly IUnitOfWork _work;

    public GetTicketByIdQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<UserTicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        var ticket = await _work.GenericRepository<Domain.Aggregates.TicketAggregate.Ticket>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (ticket == null) throw new NotFoundException($"Ticket not Found{request.Id}");
        ticket.TicketMessages = ticket.TicketMessages.OrderByDescending(x => x.Created).ToList();
        return ticket.ToDto<UserTicketDto>();
    }
}