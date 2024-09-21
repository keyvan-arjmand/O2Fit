using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using Identity.V2.Application.Countries.V1.Queries.GetCountryByCurrencyCode;

namespace Identity.V2.Application.Consumers;

public class GetCurrencyByCurrencyCodeConsumer : IConsumer<GetCountryByCurrencyCode>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public GetCurrencyByCurrencyCodeConsumer(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<GetCountryByCurrencyCode> context)
    {
        var countryDto = await _mediator.Send(new GetCountryByCurrencyCodeQuery(context.Message.CurrencyCode));
        if (countryDto == null)
        {
            var nullResponse = new GetCountryByCurrencyCodeResult()
            {
                Id = string.Empty
            };
            await context.RespondAsync<GetCountryByCurrencyCodeResult>(nullResponse);

        }
        var result = _mapper.Map<GetCountryByCurrencyCodeResult>(countryDto);

        await context.RespondAsync<GetCountryByCurrencyCodeResult>(result);
    }
}