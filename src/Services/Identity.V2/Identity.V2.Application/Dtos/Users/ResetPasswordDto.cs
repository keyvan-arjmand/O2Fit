namespace Identity.V2.Application.Dtos.Users;

public class ResetPasswordDto
{
    public string Username { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;

}