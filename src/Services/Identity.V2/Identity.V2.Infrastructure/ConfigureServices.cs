namespace Identity.V2.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {


        MongoDbPersistence.Configure();
        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")));

        #region Mongodb Migration Configuration

        var client = new MongoClient(configuration["MongoSettings:ConnectionString"]);

        services.AddSingleton<IMongoClient>(client);

        services.AddMigration(new MongoMigrationSettings
        {
            ConnectionString = configuration["MongoSettings:ConnectionString"],
            Database = configuration["MongoSettings:DatabaseName"]
        });

        #endregion
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IResponseCacheService, ResponseCacheService>();
        services.AddScoped<ISmsService, SmsService>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFileService, FileService>();
        return services;
    }
}
