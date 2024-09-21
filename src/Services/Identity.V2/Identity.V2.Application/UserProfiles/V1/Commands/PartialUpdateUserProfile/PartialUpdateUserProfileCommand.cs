namespace Identity.V2.Application.UserProfiles.V1.Commands.PartialUpdateUserProfile;

public record PartialUpdateUserProfileCommand(string UserId, string? FullName, string? Image, int HeightSize, FoodHabit FoodHabit,
    Gender Gender, DateTime BirthDate , DailyActivityRate DailyActivityRate): IRequest;