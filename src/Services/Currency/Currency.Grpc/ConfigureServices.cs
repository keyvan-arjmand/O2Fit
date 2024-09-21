using Currency.Api.Services;
using Currency.Application.Common.Interfaces.Services;

namespace Currency.Grpc;

public static class ConfigureServices
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        return services;
    }
}