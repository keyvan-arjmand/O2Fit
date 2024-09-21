using EventBus.Messages.Events.Services.Tickets;
using MassTransit;
using Ticket.Application.Messages.V1.Commands.InsertUserMessage;
using Ticket.Domain.Enums;

namespace Ticket.Application.Consumers;

public class InsertMessageConsumer : IConsumer<InsertUserMessage>
{
    private readonly IMediator _mediator;

    public InsertMessageConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<InsertUserMessage> context)
    {
        //for user messaging
        await _mediator.Send(new InsertUserMessageCommand
        {
            UserId = context.Message.UserId,
            Description = context.Message.Description,
            Title = context.Message.Title,
            IsGeneral = false,
            IsForce = false,
            UserType = UserType.User,
            Classification = (Classification)context.Message.Classification,
        }, context.CancellationToken);
    }
}