using Currency.Application;
using Currency.Application.Common.Behaviours;
using Currency.Application.Common.Interfaces.Services;
using Currency.Infrastructure;
using Currency.Infrastructure.Services;
using Microsoft.AspNetCore.TestHost;
using StackExchange.Redis;


namespace Payment.IntegrationTests;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();


            configurationBuilder.AddConfiguration(integrationConfig);
            Testing.Configuration = integrationConfig;
        });
        var entryAssembly = AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(f => f.FullName!.Contains("Currency.Application"));
        builder.ConfigureTestServices(x =>
        {
            x.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(IMediarMarker).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            });
            x.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(Testing.Configuration.GetConnectionString("RedisConnection")));
            x.AddScoped<Currency.Application.Common.Interfaces.Persistence.UoW.IUnitOfWork,
                Currency.Infrastructure.Persistence.UoW.UnitOfWork>();
            x.AddScoped<IResponseCacheService, ResponseCacheService>();
        });
    }
}