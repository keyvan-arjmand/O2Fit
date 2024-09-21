using Currency.Infrastructure.Persistence;
using Currency.Infrastructure.Persistence.UoW;
using Currency.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Currency.Infrastructure;

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
