namespace EventBus.Messages.Contracts.Services.Currencies.Currencies;

public class GetCountryByCurrencyCodeResult
{
    public string Id { get; init; }
    public string StateId { get; init; }
    public string Alpha { get; init; }
    public int CountryCode { get; init; }
    public string CurrencyCode { get; init; }
    public string CurrencyName { get; init; }
}
