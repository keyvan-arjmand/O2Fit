namespace Currency.Application.Common.Interfaces.Services;

public interface IResponseCacheService
{
    public Task CacheResponseAsync(string cacheKey, object? response, TimeSpan timeToLive);
    public Task<string?> GetCachedResponseAsync(string cacheKey);
    public Task DeleteKeyAsync(string cacheKey);
}