namespace EventBus.Messages.Contracts.Services.Identity;

public record GetCountriesInfoResult
{
    public List<CountryInfo> CountryInfos { get; init; }
}