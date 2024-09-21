using Currency.Domain.Aggregates.CurrencyAggregate;
using Currency.Domain.Common;

namespace Currency.Domain.ValueObjects;

public class CountryCurrency : BaseEntity
{
    public Translation CurrencyTranslationName { get; set; } = default!; //ریال
    public string Alpha { get; set; } = string.Empty; //IRN
    public int CountryCode { get; set; } //364
    public int CountryId { get; set; } //78
    public string CurrencyCode { get; set; } = string.Empty; //IRR
    public string CurrencyName { get; set; } = string.Empty; //Rial
    public double CoefficientCurrency { get; set; } //45932.863669 Riyal => 1 Usd
    public string UtcTime { get; set; } = string.Empty; //+04:30
}