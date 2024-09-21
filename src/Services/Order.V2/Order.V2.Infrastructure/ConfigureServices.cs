using Microsoft.Extensions.Configuration;
using Mongo.Migration.Startup.DotNetCore;
using Mongo.Migration.Startup;
using Mongo.Migration.Startup.Static;
using Mongo.Migration.Documents;

namespace Order.V2.Infrastructure;

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
