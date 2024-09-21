using OpenIddict.Abstractions;

namespace Currency.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.GetClaim(OpenIddictConstants.Claims.Subject);
    public string? Username => _httpContextAccessor.HttpContext?.User?.GetClaim(OpenIddictConstants.Claims.Name);
    public string? CountryId => _httpContextAccessor.HttpContext?.User?.GetClaim(OpenIddictConstants.Claims.Country);

    public void InsertAuditLog(AggregateRoot aggregateRoot)
    {
        aggregateRoot.Created = DateTime.UtcNow;
        aggregateRoot.CreatedById = ObjectId.Parse(UserId);
        aggregateRoot.CreatedBy = Username;
    }
      
    public void InsertAuditLog(IEnumerable<AggregateRoot> aggregateRoots)
    {
        foreach (var aggregateRoot in aggregateRoots)
        {
            aggregateRoot.Created = DateTime.UtcNow;
            aggregateRoot.CreatedById = ObjectId.Parse(UserId);
            aggregateRoot.CreatedBy = Username;    
        }
    }
    
    public void UpdateAuditLog(AggregateRoot aggregateRoot)
    {
        aggregateRoot.LastModified = DateTime.UtcNow;
        aggregateRoot.LastModifiedById = ObjectId.Parse(UserId);
        aggregateRoot.LastModifiedBy = Username;
    }
}