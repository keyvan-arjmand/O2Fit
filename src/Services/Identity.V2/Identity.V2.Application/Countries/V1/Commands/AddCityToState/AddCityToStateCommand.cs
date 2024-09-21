namespace Identity.V2.Application.Countries.V1.Commands.AddCityToState;

public record AddCityToStateCommand(string CountryId, string StateId, CreateTranslationDto Name) : IRequest;