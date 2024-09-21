namespace EventBus.Messages.Contracts.Services.Identity;

public record CountryInfo
{
    public string Id { get; init; }
    public int OldSystemId { get; init; }
    public string CurrencyName { get; init; }
    public string CountryName { get; init; }
}