namespace Identity.V2.Application.UserProfiles.V1.Commands.IncreasePkDietDateByUserId;

public record IncreasePkDietDateByUserIdCommand(string UserId, DateTime DietExpireDate): IRequest;