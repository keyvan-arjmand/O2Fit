using Ticket.Application.Common.Interfaces.Persistence.UoW;
using Ticket.Application.Dtos;
using Ticket.Domain.Aggregates.MessageAggregate;

namespace Ticket.Application.Messages.V1.Commands.InsertUserMessage;

public class InsertUserMessageCommandHandler : IRequestHandler<InsertUserMessageCommand>
{
    private readonly IUnitOfWork _work;

    public InsertUserMessageCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(InsertUserMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new Message
        {
            Classification = request.Classification,
            IsForce = request.IsForce,
            UserType = request.UserType,
            IsUserRead = false,
            Title = request.Title,
            Link = request.Link,
            IsGeneral = request.IsGeneral,
            Description = request.Description,
            UserId = request.UserId!.StringToObjectId(),
        };
        await _work.GenericRepository<Message>().InsertOneAsync(message, null, cancellationToken);
    }
}