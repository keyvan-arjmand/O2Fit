using Currency.Application.Dtos;

namespace Currency.Application.Currencies.V1.Query.GetByIdCurrency;

public record GetByIdCurrencyQuery(string Id) : IRequest<CurrencyDto>;
