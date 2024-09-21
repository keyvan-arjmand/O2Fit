namespace Identity.V2.Application.Dtos.Users;

public class UserForUserInfoDto
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    public dynamic CountryId { get; set; }
    public string ReferralCode { get; set; } = string.Empty;
    public string ReferralInviter { get; set; } = string.Empty;
    public DayOfWeek StartOfWeek { get; set; }
}