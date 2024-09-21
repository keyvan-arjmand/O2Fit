namespace Chat.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? Username => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
    public string? FullName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(CustomClaimsConstants.FullNameClaim);
    public string? Language => _httpContextAccessor.HttpContext?.User?.FindFirstValue(CustomClaimsConstants.LanguageClaim);
}