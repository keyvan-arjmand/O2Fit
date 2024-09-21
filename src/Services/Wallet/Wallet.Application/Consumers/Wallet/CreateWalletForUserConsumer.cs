using EventBus.Messages.Events.Services.Wallet;
using MassTransit;
using Wallet.Application.Wallets.V1.Command.CreateWallet;

namespace Wallet.Application.Consumers.Wallet;

public class CreateWalletForUserConsumer : IConsumer<CreatedWalletForUser>
{
    private readonly IMediator _mediator;

    public CreateWalletForUserConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CreatedWalletForUser> context)
    {
        await _mediator.Send(new CreateWalletCommand
        {
            CurrencyCode = context.Message.CurrencyCode,
            CountryId = context.Message.CountryId,
            UserId = context.Message.UserId,
            UserType = context.Message.UserType,
            UserName = context.Message.UserName,
        });
    }
}