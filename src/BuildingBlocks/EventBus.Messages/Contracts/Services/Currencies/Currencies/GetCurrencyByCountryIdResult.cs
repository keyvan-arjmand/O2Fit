namespace EventBus.Messages.Contracts.Services.Currencies.Currencies;

public class GetCurrencyByCountryIdResult
{
    public string Id { get; init; }
    public string CurrencyCode { get; init; }
    public int CountryCode { get; init; }
    public List<int> CountryIds { get; init; }
    public double CoefficientCurrency { get; init; } //45932.863669 Riyal => 1 Usd
    public string Alpha { get; init; }
}