namespace Identity.V2.Application.Countries.V1.Commands.UpdateUnCountryInfo;

public record UpdateUnCountryInfoCommand(string Id, string Alpha, string CountryCode, string Culture, string CurrencyCode,
    string CurrencyName, string UtcTime) : IRequest;