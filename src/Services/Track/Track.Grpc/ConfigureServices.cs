using Track.Application.Common.Interfaces.Services;

namespace Track.Grpc;

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