namespace Identity.V2.Application.Users.V1.Commands.UpdateStateIdAndCityId;

public record UpdateStateIdAndCityIdCommand(string UserId, string StateId, string CityId): IRequest;