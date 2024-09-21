using Chat.Application.Groups.V1.Commands.CreateGroup;

namespace Chat.Application.Consumers;

public class ChatCreatedConsumer : IConsumer<ChatCreated>
{
    private readonly IMediator _mediator;

    public ChatCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(ConsumeContext<ChatCreated> context)
    {
        return _mediator.Send(new CreateGroupCommand(context.Message.OrderId, context.Message.UserId, context.Message.NutritionistId, context.Message.UserFullName,
            context.Message.NutritionistFullName), context.CancellationToken);
    }
}