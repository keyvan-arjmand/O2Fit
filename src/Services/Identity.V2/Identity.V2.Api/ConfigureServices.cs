using Identity.V2.Api.Services;
using Mongo.Migration.Documents;
using Mongo.Migration.Startup;
using Mongo.Migration.Startup.DotNetCore;

namespace Identity.V2.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();
        #region RateLimit

        // services.AddRateLimiter(cfg =>
        // {
        //     cfg.RejectionStatusCode = 429;
        //     cfg.AddRedisFixedWindowLimiter(policyName: O2fitIdentityConstants.RateLimitPolicyName, (options) =>
        //     {
        //         options.ConnectionMultiplexerFactory = () =>
        //             ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
        //         options.PermitLimit = 5;
        //         options.Window = TimeSpan.FromSeconds(60);
        //     });
        // });

        #endregion

        services.AddCors();
        services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        services.AddAuthorization();
        services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

        //services.AddFluentValidationAutoValidation();

        services.AddHttpClient();
        
        #region Swagger

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1.0",
                Title = "Identity V2 Api V1",
                Description = "Identity V2 Api V1",
            });
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2.0",
                Title = "Identity V2 Api V2",
                Description = "Identity V2 Api V2",
            });

            options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(configuration["Swagger:LocalAuthorize"]!),
                        TokenUrl = new Uri(configuration["Swagger:LocalToken"]!),
                        Scopes = new Dictionary<string, string>()
                    },
                },
                OpenIdConnectUrl = new Uri(configuration["Swagger:LocalWellKnown"]!)
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
                .FirstOrDefault(f => f.FullName!.Contains("Identity.V2.Application"));
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

        services.AddHostedService<Worker>();

        #region Migration

        var client = new MongoClient(configuration.GetSection("MongoSettings:ConnectionString").Value);

        services.AddSingleton<IMongoClient>(client);

        services.AddMigration(new MongoMigrationSettings
        {
            ConnectionString = configuration["MongoSettings:ConnectionString"],
            Database = configuration["MongoSettings:DatabaseName"],
            DatabaseMigrationVersion = new DocumentVersion(0,1,6)
        });
        

        #endregion
        return services;
    }
}