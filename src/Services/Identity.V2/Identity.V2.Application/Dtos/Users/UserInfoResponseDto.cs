using Identity.V2.Application.Dtos.UserTrackSpecification;

namespace Identity.V2.Application.Dtos.Users;

public class UserInfoResponseDto
{
    public UserForUserInfoDto User { get; set; } = null!;
    public ProfileForUserInfoDto Profile { get; set; } = null!;
    public TargetDto Target { get; set; } = null!;
    public List<UserTrackSpecificationDto> TrackSpecification { get; set; } = null!;
}