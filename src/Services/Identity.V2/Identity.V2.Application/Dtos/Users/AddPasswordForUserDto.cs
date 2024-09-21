namespace Identity.V2.Application.Dtos.Users;

public class AddPasswordForUserDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}