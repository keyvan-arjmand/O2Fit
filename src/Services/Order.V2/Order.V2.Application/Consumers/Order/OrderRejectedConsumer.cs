using EventBus.Messages.Events.Services.Nutritionist.Order;
using MassTransit;
using Order.V2.Application.Orders.V1.Command.UpdateOrder;
using Order.V2.Domain.Enums;

namespace Order.V2.Application.Consumers.Order;

public class OrderRejectedConsumer : IConsumer<OrderRejected>
{
    private readonly IMediator _mediator;

    public OrderRejectedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }


    public async Task Consume(ConsumeContext<OrderRejected> context)
    {
        await _mediator.Send(new UpdateOrderCommand
        {
            Id = context.Message.OrderId,
            OrderState = OrderState.Rejected,
            Username = context.Message.Username,
            UserId = context.Message.UserId
        });
    }
}