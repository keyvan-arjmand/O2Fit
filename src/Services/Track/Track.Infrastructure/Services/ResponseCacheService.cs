using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Track.Infrastructure.Services;

public class ResponseCacheService : IResponseCacheService
{
    private readonly IDatabase _database;

    public ResponseCacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }
    public async Task CacheResponseAsync(string cacheKey, object? response, TimeSpan timeToLive)
    {
        if (response is null)
            return;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var serializedResponse = JsonSerializer.Serialize(response, options);

        await _database.StringSetAsync(cacheKey, serializedResponse, timeToLive).ConfigureAwait(false);
    }

    public async Task<string?> GetCachedResponseAsync(string cacheKey)
    {
        var cachedResponse = await _database.StringGetAsync(cacheKey).ConfigureAwait(false);

        if (cachedResponse.IsNullOrEmpty)
            return null;

        return cachedResponse;
    }
}