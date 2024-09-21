using Currency.Application.Dtos;
using Currency.Domain.ValueObjects;

namespace Currency.Application.Currencies.V1.Commands.CreateCurrency;

public class CreateCurrencyCommand : IRequest
{
    public string CurrencyCode { get; set; } = string.Empty;
    public string CurrencyName { get; set; } = string.Empty;
    public List<int> CountryIds { get; set; } = default!;
    public double CoefficientCurrency { get; set; } //45932.863669 Riyal => 1 Usd
    public TranslationDto Name { get; set; } = default!;
}