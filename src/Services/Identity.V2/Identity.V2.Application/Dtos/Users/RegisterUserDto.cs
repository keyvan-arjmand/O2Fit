namespace Identity.V2.Application.Dtos.Users;

public class RegisterUserDto
{
    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Password { get; set; }

    public string Username { get; set; } = string.Empty;

    public int CountryId { get; set; }

    public Language Language { get; set; }

    public string? ReferralInviter { get; set; }

    public DayOfWeek StartOfWeek { get; set; }
    public bool IsNutritionist { get; set; }
}