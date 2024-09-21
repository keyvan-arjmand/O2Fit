namespace Identity.V2.Application.Common.Interfaces.Services;

public interface IRoleService
{
    List<Role> GetList();
    Task<Role?> GetByIdAsync(string id);
    bool FindByDisplayName(string displayName);
    Task<Role?> GetByNameAsync(string name);
    Task<IdentityResult> UpdateAsync(Role role);
    Task<IdentityResult> DeleteAsync(Role role);
    Task<IdentityResult> CreateAsync(Role role);
    Task<IList<string>> GetPermissionsOfRolesByNames(IList<string> roleNames);
}