namespace Identity.V2.Application.Users.V1.Commands.ChangeIsCompleteValue;

public record ChangeIsCompleteValueCommand(string UserId, bool IsComplete): IRequest;