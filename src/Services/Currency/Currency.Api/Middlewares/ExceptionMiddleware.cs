namespace Currency.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            if (ex.StackTrace != null)
            {
                var response = _env.IsDevelopment()
                    ? new ApiResult(ex.StackTrace!, ApiResultStatusCode.ServerError, false)
                    : new ApiResult(string.Empty, ApiResultStatusCode.ServerError, false);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);
                await httpContext.Response.WriteAsync(json).ConfigureAwait(false);
            }
            else
            {
                var response = new ApiResult(string.Empty, ApiResultStatusCode.ServerError, false);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);
                await httpContext.Response.WriteAsync(json).ConfigureAwait(false);
            }
        }
    }
}
