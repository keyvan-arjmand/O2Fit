namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateFastingMode;

public record UpdateFastingModeCommand(string UserId, bool FastingMode): IRequest;