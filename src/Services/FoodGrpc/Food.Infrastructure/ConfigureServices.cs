namespace Food.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FoodContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Npgsql"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")));
        services.AddScoped<IResponseCacheService, ResponseCacheService>();
        return services;
    }
}
