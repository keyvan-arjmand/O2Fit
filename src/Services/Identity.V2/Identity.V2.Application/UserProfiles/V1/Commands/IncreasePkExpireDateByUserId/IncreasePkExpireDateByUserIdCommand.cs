namespace Identity.V2.Application.UserProfiles.V1.Commands.IncreasePkExpireDateByUserId;

public record IncreasePkExpireDateByUserIdCommand(string UserId, DateTime PkExpireDate): IRequest;