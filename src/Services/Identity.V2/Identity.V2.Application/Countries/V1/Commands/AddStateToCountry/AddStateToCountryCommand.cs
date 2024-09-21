namespace Identity.V2.Application.Countries.V1.Commands.AddStateToCountry;

public record AddStateToCountryCommand(string CountryId, CreateTranslationDto Name) : IRequest;