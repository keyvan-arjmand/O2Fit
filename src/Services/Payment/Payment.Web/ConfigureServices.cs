using Payment.Application.Common.Interfaces.Services;
using Payment.Web.Services;

namespace Payment.Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebMvcServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();
        services.AddControllersWithViews();
        services.AddMassTransit(x =>
        {
            var entryAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x => x.FullName.Contains("Payment.Application"));
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumers(entryAssembly);
            x.UsingRabbitMq((context, cfg) =>
            {
                //cfg.Host(new Uri(configuration["RabbitMqSettings:Uri"]));
                cfg.Host(configuration["RabbitMqSettings:Host"], configuration["RabbitMqSettings:VirtualHost"], h =>
                {
                    h.Username(configuration["RabbitMqSettings:Username"]);
                    h.Password(configuration["RabbitMqSettings:Password"]);
                });
                //cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
                //cfg.UseMessageRetry(r => r.Immediate(5));
                //cfg.UseInMemoryOutbox();
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
  
}