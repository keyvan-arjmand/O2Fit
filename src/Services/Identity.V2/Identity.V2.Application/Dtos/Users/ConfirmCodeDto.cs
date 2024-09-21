namespace Identity.V2.Application.Dtos.Users;

public class ConfirmCodeDto
{
    public string Username { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}