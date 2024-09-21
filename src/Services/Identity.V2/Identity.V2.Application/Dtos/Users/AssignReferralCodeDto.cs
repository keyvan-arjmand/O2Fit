namespace Identity.V2.Application.Dtos.Users;

public class AssignReferralCodeDto
{
    public string UserId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}