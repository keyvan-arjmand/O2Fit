namespace Identity.V2.Application.Countries.V1.Commands.EditStateInCountry;

public record EditStateInCountryCommand(string CountryId, string StateId,  CreateTranslationDto Name) : IRequest;