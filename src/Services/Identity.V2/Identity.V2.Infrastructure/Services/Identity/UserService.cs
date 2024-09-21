namespace Identity.V2.Infrastructure.Services.Identity;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> AssignRoleToUserAsync(AssignRoleToUserDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId).ConfigureAwait(false);
        if (user != null)
        {
            var result = await _userManager.AddToRoleAsync(user, dto.RoleName).ConfigureAwait(false);
            return result;
        }
        var errorMessage = new IdentityError
        {
            Description = "user not found"
        };
        return IdentityResult.Failed(errorMessage);
    }

    public async Task<IdentityResult> AssignRoleToUserAsync(User user, string roleName)
    {
        var result = await _userManager.AddToRoleAsync(user, roleName).ConfigureAwait(false);
        return result;
    }

    public async Task<string> GetUserIdByUsernameAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);
        if (user != null)
        {
            return user.UserName;
        }

        return string.Empty;
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
        return user;
    }

    public  Task<IdentityResult> CreateUserWithPasswordAsync(User user, string password)
    {
        return _userManager.CreateAsync(user, password);
    }

    public Task<IdentityResult> CreateUserWithoutPasswordAsync(User user)
    {
        return _userManager.CreateAsync(user);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task<IList<string>> GetUserRolesAsync(User user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public Task<IdentityResult> UpdateUserAsync(User user)
    {
        return _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> AddPasswordForUserAsync(string username, string password)
    {

        var user = await GetUserByUsernameAsync(username);
        if (user != null)
        {
            var result = await _userManager.AddPasswordAsync(user, password);
            return result;
        }
        var errorMessage = new IdentityError
        {
            Description = "user not found"
        };
        return IdentityResult.Failed(errorMessage);
    }

    public async Task<IdentityResult> UpdateSecurityStampByUsernameAsync(string username)
    {
        var user = await GetUserByUsernameAsync(username);
        if (user != null)
        {
            var result = await _userManager.UpdateSecurityStampAsync(user);
            await BlockUserAsync(user);
            return result;
        }
        var errorMessage = new IdentityError
        {
            Description = "user not found"
        };
        return IdentityResult.Failed(errorMessage);
    }

    public async Task<IdentityResult> UpdateSecurityStampByUserIdAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);
        if (user != null)
        {
            var result = await _userManager.UpdateSecurityStampAsync(user);
            await BlockUserAsync(user);
            return result;
        }
        var errorMessage = new IdentityError
        {
            Description = "user not found"
        };
        return IdentityResult.Failed(errorMessage);
    }

    public async Task BlockUserAsync(User user)
    {
            user.IsBlocked = true;
            await UpdateUserAsync(user);
    }

    public Task<IdentityResult> ChangeLockoutDateAsync(User user, DateTimeOffset dateTimeOffset)
    {
        return _userManager.SetLockoutEndDateAsync(user, dateTimeOffset);
    }

    public Task<string> GeneratePasswordResetTokenAsync(User user)
    {
        return _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
    {
        return _userManager.ResetPasswordAsync(user, token, newPassword);
    }
}