using Discount.Application.DiscountO2Fits.V1.Commands.SubtractUsableDiscountO2Fit;
using EventBus.Messages.Events.Services.Discount.Command.SubTrackUsableCountDiscount;
using MassTransit;

namespace Discount.Application.Consumers.Discounts;

public class SubTrackUsableCountDiscountO2FitConsumer : IConsumer<SubTrackedUsableCountDiscountO2FitEvent>
{
    private readonly IMediator _mediator;

    public SubTrackUsableCountDiscountO2FitConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<SubTrackedUsableCountDiscountO2FitEvent> context)
    {
        await _mediator.Send(new SubtractUsableDiscountO2FitCommand
        {
            Code = context.Message.DiscountCode,
            Username = context.Message.Username,
            UserId = context.Message.UserId,
        });
    }
}