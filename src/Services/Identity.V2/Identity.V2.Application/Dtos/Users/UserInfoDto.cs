using Identity.V2.Application.Dtos.UserTrackSpecification;

namespace Identity.V2.Application.Dtos.Users;

public class UserInfoDto: IDto
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string CountryId { get; set; } = string.Empty;
    public int Country { get; set; }
    public string ReferralCode { get; set; } = string.Empty;
    public string ReferralInviter { get; set; } = string.Empty;
    public DayOfWeek StartOfWeek { get; set; }
    public ProfileDto UserProfile { get; set; } = null!;
    public List<UserTrackSpecificationDto> UserTrackSpecification { get; set; } = null!;
}