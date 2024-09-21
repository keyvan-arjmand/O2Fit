namespace Identity.V2.Application.Dtos.Users;

public class UserDataDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public string CountryName { get; set; } = string.Empty;
    public string ReferralCode { get; set; } = string.Empty;
    public string ReferralInviter { get; set; } = string.Empty;
    public DayOfWeek StartDayOfWeek { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    
    //city
    
}