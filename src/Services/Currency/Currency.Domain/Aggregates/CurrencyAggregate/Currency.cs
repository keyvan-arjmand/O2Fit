using Currency.Domain.Common;
using Currency.Domain.ValueObjects;

namespace Currency.Domain.Aggregates.CurrencyAggregate;

public class Currency : AggregateRoot
{
    public Translation CurrencyTranslationName { get; set; } = default!;
    public List<int> CountryIds { get; set; } = default!; //{78,128}
    public CurrencyCode CurrencyCode { get; set; } = default!; //IRR
    public string CurrencyName { get; set; } = string.Empty; //Rial
    public double CoefficientCurrency { get; set; } //45932.863669 Riyal => 1 Usd
}