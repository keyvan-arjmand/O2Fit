namespace Market.Application.Common.Interfaces.Services;

public interface IResponseCacheService
{
    Task CacheResponseAsync(string cacheKey, object? response, TimeSpan timeToLive);
    Task<string?> GetCachedResponseAsync(string cacheKey);
    Task ReplaceToKey(string cacheKey, object? response, TimeSpan timeToLive);
}