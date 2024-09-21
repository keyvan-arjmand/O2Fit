using Ticket.Application.Common.Exceptions;
using Ticket.Application.Common.Interfaces.Persistence.UoW;
using Ticket.Domain.Aggregates.MessageAggregate;

namespace Ticket.Application.Messages.V1.Commands.SoftDeleteUserMessage;

public class SoftDeleteUserMessageCommandHandler : IRequestHandler<SoftDeleteUserMessageCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteUserMessageCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteUserMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _work.GenericRepository<Message>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (message == null)
            throw new NotFoundException(nameof(Message), request.Id);
        await _work.GenericRepository<Message>()
            .SoftDeleteByIdAsync(request.Id, message, null, cancellationToken);
    }
}