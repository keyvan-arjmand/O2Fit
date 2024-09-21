using Currency.Application.Currencies.V1.Query.GetByCountryId;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Application.Consumers.Currencies
{
    public class GetCurrencyByCountryIdConsumer : IConsumer<GetCurrencyByCountryId>
    {
        private readonly IMediator _mediator;

        public GetCurrencyByCountryIdConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<GetCurrencyByCountryId> context)
        {
            var currency = await _mediator.Send(new GetCurrencyByCountryIdQuery
            {
                CountryIds = context.Message.CountryIds
            });
            if (currency != null)
            {
                await context.RespondAsync(new GetCurrencyByCountryIdResult
                {
                    Id = currency.Id,
                    CountryCode = currency.CountryCode,
                    CountryIds = currency.CountryIds,
                    Alpha = currency.Alpha,
                    CoefficientCurrency = currency.CoefficientCurrency,
                    CurrencyCode = currency.CurrencyCode,
                });

            }
            else
            {
                await context.RespondAsync(new GetCurrencyByCountryIdResult
                {
                    Id = string.Empty,
                    CurrencyCode = string.Empty
                });
            }
        }
    }
}
