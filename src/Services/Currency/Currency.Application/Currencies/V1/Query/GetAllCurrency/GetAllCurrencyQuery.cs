using Currency.Application.Dtos;

namespace Currency.Application.Currencies.V1.Query.GetAllCurrency;

public record GetAllCurrencyQuery() : IRequest<List<CurrencyDto>>;
