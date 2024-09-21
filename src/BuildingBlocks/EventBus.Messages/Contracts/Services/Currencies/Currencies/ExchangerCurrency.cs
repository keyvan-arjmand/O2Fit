namespace EventBus.Messages.Contracts.Services.Currencies.Currencies;

public class ExchangerCurrency
{
    public string SourceCurrencyCode { get; init; }
    public double SourceCurrencyAmount { get; init; }
    public string DestinationCurrencyCode { get; init; }
}