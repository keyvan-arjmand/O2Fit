﻿namespace Discount.Api;

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
                Title = "Discount Api V1",
                Description = "Discount Api V1",
            });
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2.0",
                Title = "Discount Api V2",
                Description = "Discount Api V2",
            });
            options.CustomSchemaIds(x => x.FullName);
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
                            {"discount_api","discount_api"}
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

        #region Masstranssit

        services.AddMassTransit(x =>
        {
            var entryAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x => x.FullName.Contains("Discount.Application"));
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumers(entryAssembly);
            x.UsingRabbitMq((context, cfg) =>
            {
                // cfg.Host(new Uri(configuration["RabbitMqSettings:Uri"]));
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
                //options.SetIssuer("https://localhost:6012/");
                options.SetIssuer(configuration["IdentitySettings:Issuer"]!);

                // options.AddAudiences("release_v2");
                options.AddAudiences(configuration["IdentitySettings:Audiences:DiscountResource"]!);

                // Configure the validation handler to use introspection and register the client
                // credentials used when communicating with the remote introspection endpoint.
                // options.UseIntrospection()
                //   .SetClientId("release_v2")
                // .SetClientSecret("D0A32D54-4EC6-46D7-A2B7-804157443129");
       

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