namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateUserProfileTargetNutrient;

public record UpdateUserProfileTargetNutrientCommand(string UserId, List<double> TargetNutrient) : IRequest;