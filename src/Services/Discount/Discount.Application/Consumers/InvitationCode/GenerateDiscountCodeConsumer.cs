using Discount.Application.DiscountO2Fits.V1.Commands.GenerateInvitationCode;
using EventBus.Messages.Events.Services.Discount.Command.DiscountInvitationCode;
using MassTransit;

namespace Discount.Application.Consumers.InvitationCode;

public class GenerateDiscountCodeConsumer : IConsumer<CreatedDiscountInvitationCode>
{
    private readonly IMediator _mediator;

    public GenerateDiscountCodeConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CreatedDiscountInvitationCode> context)
    {
        await _mediator.Send(new GenerateInvitationCodeCommand
        {
            UserId = context.Message.UserId,
            DiscountType = context.Message.DiscountType,
            Username = context.Message.Username
        });
    }
}