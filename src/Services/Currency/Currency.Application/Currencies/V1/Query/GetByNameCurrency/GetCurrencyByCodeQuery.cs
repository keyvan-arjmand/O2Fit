using Currency.Application.Dtos;

namespace Currency.Application.Currencies.V1.Query.GetByNameCurrency;

public record GetCurrencyByCodeQuery(string CurrencyCode) : IRequest<CurrencyDto>;
