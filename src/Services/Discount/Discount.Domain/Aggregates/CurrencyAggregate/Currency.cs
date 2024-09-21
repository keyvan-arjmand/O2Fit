using Discount.Domain.Common;
using Discount.Domain.ValueObjects;

namespace Discount.Domain.Aggregates.CurrencyAggregate;

public class Currency : AggregateRoot
{
    public List<int> CountryIds { get; set; } = default!; //{78,128}
    public CurrencyCode CurrencyCode { get; set; } = default!; //IRR
    public double CoefficientCurrency { get; set; } //45932.863669 Riyal => 1 Usd
}