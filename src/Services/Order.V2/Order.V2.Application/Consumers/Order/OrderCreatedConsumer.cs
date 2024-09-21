using EventBus.Messages.Events.Services.Order;
using MassTransit;
using Order.V2.Application.Orders.V1.Command.InsertOrder;

namespace Order.V2.Application.Consumers.Order;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IMediator _mediator;

    public OrderCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        await _mediator.Send(new InsertOrderCommand
        {
            PaymentTransactionId = context.Message.PaymentTransactionId,
            WalletTransactionId = context.Message.WalletTransactionId,
            Username = context.Message.Username,
            UserId = context.Message.UserId
            
        });
    }
}