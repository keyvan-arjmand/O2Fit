using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Wallet;
using MassTransit;
using Wallet.Application.Wallets.V1.Query.GetWalletByUserId;

namespace Wallet.Application.Consumers.Wallet;

public class GetWalletByUserIdConsumer : IConsumer<GetWalletByUserId>
{
    private readonly IMediator _mediator;

    public GetWalletByUserIdConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<GetWalletByUserId> context)
    {
        var wallet = await _mediator.Send(new GetWalletByUserIdQuery
        {
            UserId = context.Message.UserId
        });
        if (wallet != null)
        {
            await context.RespondAsync(new GetWalletByUserIdResult
            {
                Amount = wallet.Amount,
                CurrencyCode = wallet.CurrencyCode,
                CurrencyId = wallet.CurrencyId,
                Id = wallet.Id,
            });
        }
        else
        {
            await context.RespondAsync(new GetWalletByUserIdResult
            {
                Amount = 0,
                CurrencyCode = string.Empty,
                CurrencyId = string.Empty,
                Id = string.Empty,
            });
        }
    }
}