using FluentValidation.AspNetCore;
using Identity.Api.Services;
using Identity.Application.Common.Interface;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Identity.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        services.AddFluentValidationAutoValidation();
        services.AddCors();
        services.AddHttpClient();
        #region Swagger
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1.0",
                Title = "Identity Api V1",
                Description = "Identity Api V1",
            });
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2.0",
                Title = "Identity Api V2",
                Description = "Identity Api V2",
            });
         
        });

        #endregion
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        #region Masstansit

        services.AddMassTransit(x =>
        {
            var entryAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(f => f.FullName!.Contains("Identity.Application"));
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumers(entryAssembly);
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMqSettings:Host"], configuration["RabbitMqSettings:VirtualHost"], h =>
                {
                    h.Username(configuration["RabbitMqSettings:Username"]);
                    h.Password(configuration["RabbitMqSettings:Password"]);
                });
                cfg.ConfigureEndpoints(context);
            });
        });

        #endregion


        
        return services;
    }
  
}