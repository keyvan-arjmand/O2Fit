using System.Security.Claims;
using Payment.Application.Common.Interfaces.Services;
using Payment.Domain.Common;

namespace Payment.Grpc.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? Username => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

    public void InsertAuditLog(AggregateRoot aggregateRoot)
    {
        // aggregateRoot.Created = DateTime.UtcNow;
        // aggregateRoot.CreatedById = ObjectId.Parse(UserId);
        // aggregateRoot.CreatedBy = Username;
    }

    public void InsertAuditLog(IEnumerable<AggregateRoot> aggregateRoots)
    {
        // foreach (var aggregateRoot in aggregateRoots)
        // {
        //     aggregateRoot.Created = DateTime.UtcNow;
        //     aggregateRoot.CreatedById = ObjectId.Parse(UserId);
        //     aggregateRoot.CreatedBy = Username;    
        // }
    }
    
    public void UpdateAuditLog(AggregateRoot aggregateRoot)
    {
        // aggregateRoot.LastModified = DateTime.UtcNow;
        // aggregateRoot.LastModifiedById = ObjectId.Parse(UserId);
        // aggregateRoot.LastModifiedBy = Username;
    }
}