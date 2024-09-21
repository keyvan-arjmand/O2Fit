using EventBus.Messages.Events.Services.Identity;
using Identity.V2.Application.DeviceInfo.V1.Commands.ChangeIsProfileCompleteToTrueByUserId;

namespace Identity.V2.Application.Consumers;

public class UserProfileCreatedConsumer : IConsumer<UserProfileCreated>
{
    private readonly IMediator _mediator;

    public UserProfileCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UserProfileCreated> context)
    {
        await _mediator.Send(new ChangeIsProfileCompleteToTrueByUserIdCommand(context.Message.UserId));
    }
}