namespace Identity.V2.Application.Dtos.Users;

public class NutritionistDataDto : IDto
{
    public UserForUserInfoDto User { get; set; } = null!;
    public NutritionistProfileDto NutritionistProfile { get; set; } = null!;
}