using Currency.Domain.Aggregates.CurrencyAggregate;
using Currency.Domain.ValueObjects;

namespace Currency.Application.Dtos;

public class CurrencyDto
{
    public string Id { get; set; } = string.Empty;
    public Translation CurrencyTranslationName { get; set; } = default!; 
    public string Alpha { get; set; } = string.Empty; //IRN
    public int CountryCode { get; set; } //364
    public List<int> CountryIds { get; set; } = default!; //{78,128}
    public string CurrencyCode { get; set; } = string.Empty; //IRR
    public double CoefficientCurrency { get; set; } //45932.863669 Riyal => 1 Usd
}