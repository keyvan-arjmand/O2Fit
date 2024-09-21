namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateUserProfile;

public record UpdateUserProfileCommand(string UserId, string? FullName, string? ImageUri, FoodHabit FoodHabit, Gender Gender, int HeightSize,
    DateTime BirthDate, DailyActivityRate DailyActivityRate , double TargetWeight, int WeightChangeRate, int TargetStep, double TargetThighSize,
    double TargetNeckSize): IRequest<string>;