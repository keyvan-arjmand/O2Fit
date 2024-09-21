using System.Text;

namespace Notification.Api.Filters;

public class CachedAttribute : Attribute, IAsyncActionFilter
{
    private readonly double _timeToLive;

    public CachedAttribute(double timeToLive)
    {
        _timeToLive = timeToLive;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

        var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

        var cacheResponse = await cacheService.GetCachedResponseAsync(cacheKey).ConfigureAwait(false);
        if (!string.IsNullOrEmpty(cacheResponse))
        {
            var contentResult = new ContentResult
            {
                Content =cacheResponse,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK
            };
            context.Result = contentResult;

            return;
        }

        var executedContext = await next().ConfigureAwait(false); // move to controller

        if (executedContext.Result is OkObjectResult okObjectResult)
        {
            await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromDays(_timeToLive)).ConfigureAwait(false);
        }
    }

    private string GenerateCacheKeyFromRequest(HttpRequest httpContextRequest)
    {
        var keyBuilder = new StringBuilder();
        keyBuilder.Append($"{httpContextRequest.Path}");

        foreach (var (key, value) in httpContextRequest.Query.OrderBy(x => x.Key))
        {
            keyBuilder.Append($":{key}-{value}");
        }
        return keyBuilder.ToString();
    }
}