using AspNetCore.Identity.Mongo;
using Quartz;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Identity.V2.Api;

public static class OpenIdDictConfigureServices
{
    public static async Task<IServiceCollection> AddOpenIdDictServicesAsync(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIdentityMongoDbProvider<User, Role>(options =>
                {
                    options.User.RequireUniqueEmail = false;

                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredLength =  6;

                    options.SignIn.RequireConfirmedEmail = false;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                },
                mongo =>
                {
                    mongo.ConnectionString = configuration["MongoSettings:ConnectionString"];
                    mongo.UsersCollection = "users";
                    mongo.RolesCollection = "roles";
                    mongo.MigrationCollection = "migrations";
                }
            ).AddRoles<Role>()
            //.AddSignInManager()
            //.AddUserManager<UserManager<User>>()
            //.AddRoleManager<RoleManager<Role>>()
            .AddDefaultTokenProviders();
        // OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
        // (like pruning orphaned authorizations/tokens from the database) at regular intervals.
        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
            //options.UsePersistentStore(x =>
            //{
            //    x.UseClustering(clusterOptions =>
            //    {
            //       // clusterOptions.CheckinInterval= TimeSpan.FromMilliseconds(10_000);
            //       // clusterOptions.CheckinMisfireThreshold=TimeSpan.FromMilliseconds(10_000);
            //    });
            //    x.UsePostgres(configuration.GetConnectionString("PostgresConnection")!);
            //    x.UseBinarySerializer();
            //    x.PerformSchemaValidation = false;
            //});
            
        });

        // Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);


        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseMongoDb().UseDatabase(new MongoClient(configuration["MongoSettings:ConnectionString"]).GetDatabase("openIdDict"));
                // Enable Quartz.NET integration.
               options.UseQuartz()
                   .SetMinimumTokenLifespan(TimeSpan.FromMinutes(10))
                   .SetMinimumAuthorizationLifespan(TimeSpan.FromMinutes(10));
            })
            // Register the OpenIddict server components.
            .AddServer(options =>
            {
                //options.DisableAccessTokenEncryption();
                // Enable the token endpoint.
                options.SetAuthorizationEndpointUris("connect/authorize")
                    .SetIntrospectionEndpointUris("introspect")
                    .SetTokenEndpointUris("connect/token");
                    
                // Mark the "email", "profile" and "roles" scopes as supported scopes.
                options.RegisterScopes(OpenIddictConstants.Scopes.Email, 
                    OpenIddictConstants.Scopes.Profile,
                    OpenIddictConstants.Scopes.Roles,
                    OpenIddictConstants.Scopes.OpenId,
                    OpenIddictConstants.Scopes.OfflineAccess,
                    //OpenIddictConstants.Permissions.Prefixes.Scope + "api1",
                    OpenIddictConstants.Permissions.Prefixes.Scope + "api2",
                    OpenIddictConstants.Permissions.Prefixes.Scope +
                    configuration["AuthServerLocalhostSettings:Scopes:OrderV2:Name"],
                    OpenIddictConstants.Permissions.Prefixes.Scope +
                    configuration["AuthServerLocalhostSettings:Scopes:Payment:Name"],
                    OpenIddictConstants.Permissions.Prefixes.Scope +
                    configuration["AuthServerLocalhostSettings:Scopes:Currency:Name"],
                    OpenIddictConstants.Permissions.Prefixes.Scope +
                    configuration["AuthServerLocalhostSettings:Scopes:Discount:Name"],
                    OpenIddictConstants.Permissions.Prefixes.Scope +
                    configuration["AuthServerLocalhostSettings:Scopes:Wallet:Name"]);
                // Enable the password and the refresh token flows.
                options.AllowAuthorizationCodeFlow()
                    .RequireProofKeyForCodeExchange()
                    .AllowRefreshTokenFlow()
                    .AllowPasswordFlow()

                    #region for IIS

                    .AddEphemeralSigningKey();

                #endregion
                    //.AddEphemeralEncryptionKey()
                    //.AddEphemeralSigningKey();
                    //.AddEphemeralSigningKey();
                    
                   //options.UseReferenceAccessTokens()
                   //    .UseReferenceRefreshTokens();
                    
                   // options.UseDataProtection()
                   //     .PreferDefaultAccessTokenFormat()
                   //     .PreferDefaultAuthorizationCodeFormat()
                   //     .PreferDefaultDeviceCodeFormat()
                   //     .PreferDefaultRefreshTokenFormat()
                   //     .PreferDefaultUserCodeFormat();
                // Using reference tokens means that the actual access and refresh tokens are stored in the database
                // and a token referencing the actual tokens (in the db) is used in the request header.
                // The actual tokens are not made public.
                //options.UseReferenceAccessTokens();
                //options.UseReferenceRefreshTokens();
                
                
                options.AddEncryptionKey(new SymmetricSecurityKey(
                    Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));


                options.SetAccessTokenLifetime(TimeSpan.FromDays(7));
                options.SetIdentityTokenLifetime(TimeSpan.FromDays(7));
                options.SetRefreshTokenLifetime(TimeSpan.FromDays(30));
                
                //options.SetAccessTokenLifetime(TimeSpan.FromMinutes(1));
                //options.SetIdentityTokenLifetime(TimeSpan.FromMinutes(1));
                //options.SetRefreshTokenLifetime(TimeSpan.FromDays(30));

                // Register the signing and encryption credentials.
                //options.AddDevelopmentEncryptionCertificate()
                //    .AddDevelopmentSigningCertificate();
                
                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableTokenEndpointPassthrough()
                    .DisableTransportSecurityRequirement();
                    //.EnableAuthorizationEndpointPassthrough()
                    ////    .EnableAuthorizationEndpointPassthrough()
                    //    .EnableLogoutEndpointPassthrough()
                    //.EnableTokenEndpointPassthrough()
                    //.DisableTransportSecurityRequirement();
                //.EnableUserinfoEndpointPassthrough()
                //.EnableStatusCodePagesIntegration();
                // options.Configure(builder =>
                // {
                //     //builder.TokenValidationParameters.ValidIssuer = "https://localhost:6012/";
                //     builder.Issuer = new Uri("http://localhost:6012/");
                //     //builder.TokenValidationParameters.ValidIssuers = new List<string>
                //     //{
                //     //    "http://localhost:6012/",
                //     //    "https://identity.v2.api:6012/"
                //     //};
                // });
                options.SetIssuer(new Uri(configuration["AuthServerLocalhostSettings:Issuer"]!));
                
                // options.UseDataProtection()
                //     .PreferDefaultAccessTokenFormat()
                //     .PreferDefaultAuthorizationCodeFormat()
                //     .PreferDefaultDeviceCodeFormat()
                //     .PreferDefaultRefreshTokenFormat()
                //     .PreferDefaultUserCodeFormat();

               //var environment = services.BuildServiceProvider().GetRequiredService<IHostingEnvironment>();
               //if (string.IsNullOrWhiteSpace(environment.WebRootPath))
               //{
               //    environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
               //}
               //
               // var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
               // var path = Path.Combine(basePath,
               //     configuration["AuthServerLocalhostSettings:Ssl:PfxAddress"]!);
               //var path = Path.Combine(basePath,
                 //  "sec/aspnetapp.pfx");


                 #region For Production

                 //  using var signingCert = new FileStream(configuration["AuthServerLocalhostSettings:Ssl:SigningPfx"]!
                 //      , FileMode.Open);
                 //options.AddSigningCertificate(signingCert,string.Empty);
                 //
                 //using var encryptionCert =
                 //    new FileStream(configuration["AuthServerLocalhostSettings:Ssl:EncryptionPfx"]!, FileMode.Open);
                 //options.AddEncryptionCertificate(encryptionCert,
                 //   string.Empty);
                 

                 #endregion
               // options.AddSigningCertificate(new FileStream(path, FileMode.Open),
               //    "12345678");
               // options.AddSigningCertificate(new FileStream(configuration["AuthServerLocalhostSettings:Ssl:PfxAddress"]!, FileMode.Open),
               //     configuration["AuthServerLocalhostSettings:Ssl:PfxPassword"]!);

            })
            // Register the OpenIddict validation components.
            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                options.SetIssuer(new Uri(configuration["AuthServerLocalhostSettings:Issuer"]!));

                //options.EnableTokenEntryValidation();
                
               // options.UseDataProtection();
               // options.UseDataProtection();
                // Register the ASP.NET Core host.
                options.UseAspNetCore();

            });
        
        var provider = services.BuildServiceProvider();
        var context = provider.GetRequiredService<IOpenIddictMongoDbContext>();
        var options = provider.GetRequiredService<IOptionsMonitor<OpenIddictMongoDbOptions>>().CurrentValue;
        var database = await context.GetDatabaseAsync(CancellationToken.None).ConfigureAwait(false);

        var applications = database.GetCollection<OpenIddictMongoDbApplication>(options.ApplicationsCollectionName);
        
        await applications.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<OpenIddictMongoDbApplication>(
                Builders<OpenIddictMongoDbApplication>.IndexKeys.Ascending(application => application.ClientId),
                new CreateIndexOptions
                {
                    Unique = true
                }),
        
            new CreateIndexModel<OpenIddictMongoDbApplication>(
                Builders<OpenIddictMongoDbApplication>.IndexKeys.Ascending(application =>
                    application.PostLogoutRedirectUris),
                new CreateIndexOptions
                {
                    Background = true
                }),
        
            new CreateIndexModel<OpenIddictMongoDbApplication>(
                Builders<OpenIddictMongoDbApplication>.IndexKeys.Ascending(application => application.RedirectUris),
                new CreateIndexOptions
                {
                    Background = true
                })
        }).ConfigureAwait(false);
        
        var authorizations =
            database.GetCollection<OpenIddictMongoDbAuthorization>(options.AuthorizationsCollectionName);
        
        await authorizations.Indexes.CreateOneAsync(
            new CreateIndexModel<OpenIddictMongoDbAuthorization>(
                Builders<OpenIddictMongoDbAuthorization>.IndexKeys
                    .Ascending(authorization => authorization.ApplicationId)
                    .Ascending(authorization => authorization.Scopes)
                    .Ascending(authorization => authorization.Status)
                    .Ascending(authorization => authorization.Subject)
                    .Ascending(authorization => authorization.Type),
                new CreateIndexOptions
                {
                    Background = true
                })).ConfigureAwait(false);
        
        var scopes = database.GetCollection<OpenIddictMongoDbScope>(options.ScopesCollectionName);
        
        await scopes.Indexes.CreateOneAsync(new CreateIndexModel<OpenIddictMongoDbScope>(
            Builders<OpenIddictMongoDbScope>.IndexKeys.Ascending(scope => scope.Name),
            new CreateIndexOptions
            {
                Unique = true
            })).ConfigureAwait(false);
        
        var tokens = database.GetCollection<OpenIddictMongoDbToken>(options.TokensCollectionName);
        
        await tokens.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<OpenIddictMongoDbToken>(
                Builders<OpenIddictMongoDbToken>.IndexKeys.Ascending(token => token.ReferenceId),
                new CreateIndexOptions<OpenIddictMongoDbToken>
                {
                    // Note: partial filter expressions are not supported on Azure Cosmos DB.
                    // As a workaround, the expression and the unique constraint can be removed.
                    PartialFilterExpression =
                        Builders<OpenIddictMongoDbToken>.Filter.Exists(token => token.ReferenceId),
                    Unique = true
                }),
        
            new CreateIndexModel<OpenIddictMongoDbToken>(
                Builders<OpenIddictMongoDbToken>.IndexKeys
                    .Ascending(token => token.ApplicationId)
                    .Ascending(token => token.Status)
                    .Ascending(token => token.Subject)
                    .Ascending(token => token.Type),
                new CreateIndexOptions
                {
                    Background = true
                })
        }).ConfigureAwait(false);
        
        return services;
    }
}
