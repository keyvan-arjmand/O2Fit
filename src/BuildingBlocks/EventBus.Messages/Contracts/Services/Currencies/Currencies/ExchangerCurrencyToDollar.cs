namespace EventBus.Messages.Contracts.Services.Currencies.Currencies;

public class ExchangerCurrencyToDollar
{
    public string CurrencyCode { get; init; }
    public double CurrencyAmount { get; init; }
}