using EventBus.Messages.Events.Services.Nutritionist.Order;
using EventBus.Messages.Events.Services.Order;
using MassTransit;
using Order.V2.Application.Orders.V1.Command.UpdateOrder;
using Order.V2.Domain.Enums;

namespace Order.V2.Application.Consumers.Order;

public class OrderAcceptedConsumer : IConsumer<OrderAccepted>
{
    private readonly IMediator _mediator;

    public OrderAcceptedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<OrderAccepted> context)
    {
        await _mediator.Send(new UpdateOrderCommand
        {
            Id = context.Message.OrderId,
            OrderState = OrderState.Accepted,
            UserId = context.Message.UserId,
            Username = context.Message.Username
        });
    }
}