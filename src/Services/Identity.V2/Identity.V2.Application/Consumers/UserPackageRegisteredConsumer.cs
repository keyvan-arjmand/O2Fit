using Common.Enums.TypeEnums;
using EventBus.Messages.Events.Services.Identity.Package;
using Identity.V2.Application.UserProfiles.V1.Commands.IncreasePkDietDateByUserId;
using Identity.V2.Application.UserProfiles.V1.Commands.IncreasePkExpireDateByUserId;

namespace Identity.V2.Application.Consumers;

public class UserPackageRegisteredConsumer : IConsumer<UserPackageRegistered>
{
    private readonly IMediator _mediator;

    public UserPackageRegisteredConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UserPackageRegistered> context)
    {
        switch (context.Message.PackageType)
        {
            case PackageType.Diet:
            {
                await _mediator.Send(
                    new IncreasePkDietDateByUserIdCommand(context.Message.UserId, context.Message.ExpireDate));
                break;
            }
            
            case PackageType.CalorieCounting:
            {
                await _mediator.Send(new IncreasePkExpireDateByUserIdCommand(context.Message.UserId,
                    context.Message.ExpireDate));
                break;
            }
        }
    }
}