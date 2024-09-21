using Currency.Application.Currencies.V1.Query.GetByNameCurrency;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using MassTransit;

namespace Currency.Application.Consumers.Currencies;

public class GetCurrencyByCodeConsumer : IConsumer<GetCurrencyByCode>
{
    private readonly IMediator _mediator;
    public GetCurrencyByCodeConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task Consume(ConsumeContext<GetCurrencyByCode> context)
    {
        var currency = await _mediator.Send(new GetCurrencyByCodeQuery(context.Message.CurrencyCode));
        if (currency != null)
        {
            var result = new GetCurrencyByCodeResult
            {
                Id = currency.Id,
                CoefficientCurrency = currency.CoefficientCurrency,
                CurrencyCode = currency.CurrencyCode,
                Alpha = currency.Alpha,
                CountryCode = currency.CountryCode,
            };
            await context.RespondAsync(result);
        }
        else
        {
            await context.RespondAsync(new GetCurrencyByCodeResult
            {
                Id = string.Empty,
                CurrencyCode = string.Empty,
            });
        }
    }
}