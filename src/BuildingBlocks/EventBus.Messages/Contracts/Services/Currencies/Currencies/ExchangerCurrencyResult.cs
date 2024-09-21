namespace EventBus.Messages.Contracts.Services.Currencies.Currencies;

public class ExchangerCurrencyResult
{
    public string SourceCurrencyCode { get; init; }
    public double SourceCurrencyAmount { get; init; }
    public string DestinationCurrencyCode { get; init; }
    public double DestinationCurrencyAmount { get; init; }
    public double ExRate { get; init; }
}