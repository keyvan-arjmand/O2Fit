namespace Discount.Grpc;

public static class ConfigureServices
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();

        return services;
    }
}