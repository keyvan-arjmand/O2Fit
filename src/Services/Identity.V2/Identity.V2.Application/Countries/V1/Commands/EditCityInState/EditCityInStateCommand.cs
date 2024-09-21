namespace Identity.V2.Application.Countries.V1.Commands.EditCityInState;

public record EditCityInStateCommand(string CountryId, string StateId, string CityId , CreateTranslationDto Name) : IRequest;