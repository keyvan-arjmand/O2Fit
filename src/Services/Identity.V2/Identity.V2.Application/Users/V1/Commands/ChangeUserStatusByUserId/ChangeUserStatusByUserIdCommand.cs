namespace Identity.V2.Application.Users.V1.Commands.ChangeUserStatusByUserId;

public record ChangeUserStatusByUserIdCommand(string UserId, UserStatus Status) : IRequest;
