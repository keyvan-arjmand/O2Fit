using Currency.Application.Common.Interfaces.Persistence.UoW;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using MassTransit;

namespace Currency.Application.Consumers.Currencies;

public class GetCurrencyByIdConsumer : IConsumer<GetCurrencyById>
{
    private readonly IUnitOfWork _work;

    public GetCurrencyByIdConsumer(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Consume(ConsumeContext<GetCurrencyById> context)
    {
        var result = await _work.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>()
            .GetByIdAsync(context.Message.Id);
        await context.RespondAsync(new GetCurrencyByIdResult
        {
            CurrencyCode = result.CurrencyCode,
            CoefficientCurrency = result.CoefficientCurrency,
            CountryIds = result.CountryIds,
            Id = result.Id
        });
    }
}