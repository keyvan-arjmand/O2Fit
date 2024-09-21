namespace Identity.V2.Application.Dtos.Users;

public class UserDto : IDto
{
    public string UserName { get; set; } = default!;
    public string NormalizedUserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string NormalizedEmail { get; set; } = default!;
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public bool PhoneNumberConfirmed { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedOn { get; set; }
    //public List<Role> Roles { get; set; } = default!;
}