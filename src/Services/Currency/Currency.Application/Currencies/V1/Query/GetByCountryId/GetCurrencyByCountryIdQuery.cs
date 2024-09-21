using Currency.Application.Dtos;

namespace Currency.Application.Currencies.V1.Query.GetByCountryId;

public class GetCurrencyByCountryIdQuery : IRequest<CurrencyDto>
{
    public int CountryIds { get; set; } 
}