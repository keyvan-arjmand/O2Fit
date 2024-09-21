namespace Payment.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {


        MongoDbPersistence.Configure();
        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IResponseCacheService, ResponseCacheService>();
     
        return services;
    }
}
