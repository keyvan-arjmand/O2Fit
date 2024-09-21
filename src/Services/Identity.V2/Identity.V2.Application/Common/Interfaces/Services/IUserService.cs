namespace Identity.V2.Application.Common.Interfaces.Services;

public interface IUserService
{
    Task<IdentityResult> AssignRoleToUserAsync(AssignRoleToUserDto dto);
    Task<IdentityResult> AssignRoleToUserAsync(User user, string roleName);
    Task<string> GetUserIdByUsernameAsync(string username);
    Task<User?> GetUserByIdAsync(string userId);
    Task<IdentityResult> CreateUserWithPasswordAsync(User user, string password);
    Task<IdentityResult> CreateUserWithoutPasswordAsync(User user);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<IList<string>> GetUserRolesAsync(User user);
    Task<IdentityResult> UpdateUserAsync(User user);
    Task<IdentityResult> AddPasswordForUserAsync(string username, string password);
    Task<IdentityResult> UpdateSecurityStampByUsernameAsync(string username);
    Task<IdentityResult> UpdateSecurityStampByUserIdAsync(string userId);

    Task BlockUserAsync(User user);

    Task<IdentityResult> ChangeLockoutDateAsync(User user, DateTimeOffset dateTimeOffset);

    Task<string> GeneratePasswordResetTokenAsync(User user);
    Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
}