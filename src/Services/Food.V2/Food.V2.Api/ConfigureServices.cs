using FluentValidation.AspNetCore;
using Food.V2.Api.Services;
using Food.V2.Application.Common.Interfaces.Persistence.Repositories;
using Food.V2.Infrastructure.Persistence.Repositories;
using MassTransit;
using Microsoft.IdentityModel.Tokens;

namespace Food.V2.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        services.AddAuthorization();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        services.AddFluentValidationAutoValidation();

        #region Swagger

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1.0",
                Title = "Food V2 Api V1",
                Description = "Food V2 Api V1",
            });
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2.0",
                Title = "Food V2 V2",
                Description = "Food V2 Api V2",
            });
            options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri("https://localhost:6012/connect/authorize"),
                        TokenUrl = new Uri("https://localhost:6012/connect/token"),
                        Scopes = new Dictionary<string, string>()
                        {
                            { "food_api", "food_api" }
                        }
                    },
                },
                OpenIdConnectUrl = new Uri("https://localhost:6012/.well-known/openid-configuration")
            });
            options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "oauth2");
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

        #region Masstransit

        services.AddMassTransit(x =>
        {
            var entryAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(f => f.FullName!.Contains("Food.V2.Application"));
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

        #endregion

        #region OpenIdDict

        services.AddOpenIddict()
            .AddValidation(options =>
            {
                // Note: the validation handler uses OpenID Connect discovery
                // to retrieve the address of the introspection endpoint.
                options.SetIssuer(configuration["IdentitySettings:Issuer"]!);

                options.AddAudiences(configuration["IdentitySettings:Audiences:FoodResource"]!);

                options.AddEncryptionKey(new SymmetricSecurityKey(
                    Convert.FromBase64String(configuration["IdentitySettings:EncryptionKey"]!)));
                // Register the System.Net.Http integration.
                options.UseSystemNetHttp();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });

        #endregion

        return services;
    }
}