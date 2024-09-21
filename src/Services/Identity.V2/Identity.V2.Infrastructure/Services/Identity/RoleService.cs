using Role = Identity.V2.Domain.Aggregates.RoleAggregate.Role;

namespace Identity.V2.Infrastructure.Services.Identity;

public class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IUnitOfWork _uow;
    public RoleService(RoleManager<Role> roleManager, IUnitOfWork uow)
    {
        _roleManager = roleManager;
        _uow = uow;
    }

    public List<Role> GetList()
    {
        return _roleManager.Roles.ToList();
    }

    public Task<Role?> GetByIdAsync(string id)
    {
        return _roleManager.FindByIdAsync(id);
    }

    public bool FindByDisplayName(string displayName)
    {
        var role = _roleManager.Roles.FirstOrDefault(x => x.DisplayName == displayName);
        return role != null;
    }

    public Task<Role?> GetByNameAsync(string name)
    {
        return  _roleManager.FindByNameAsync(name);
    }

    public Task<IdentityResult> UpdateAsync(Role role)
    {
        return _roleManager.UpdateAsync(role);
    }

    public Task<IdentityResult> DeleteAsync(Role role)
    {
        return _roleManager.DeleteAsync(role);
    }

    public Task<IdentityResult> CreateAsync(Role role)
    {
        return _roleManager.CreateAsync(role);
    }

    public async Task<IList<string>> GetPermissionsOfRolesByNames(IList<string> roleNames)
    {
        if (roleNames.Any())
        {
            var permissions = new List<string>();
            foreach (var roleName in roleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                var rolePermissions = role?.Permissions.Select(s => s.Name).ToList();
                if (rolePermissions != null) 
                    permissions.AddRange(rolePermissions);
            }

            var finalList = permissions.Distinct().ToList();
            return finalList;
        }

        return new List<string>();
    }
}