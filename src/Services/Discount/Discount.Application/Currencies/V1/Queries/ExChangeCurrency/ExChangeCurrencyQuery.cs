using Discount.Application.Dtos;

namespace Discount.Application.Currencies.V1.Queries.ExChangeCurrency;

public class ExChangeCurrencyQuery:IRequest<ExChangeResult>
{
    public string SourceCurrencyCode { get; init; } = string.Empty;
    public double SourceCurrencyAmount { get; init; }
    public string DestinationCurrencyCode { get; init; } = string.Empty;
}