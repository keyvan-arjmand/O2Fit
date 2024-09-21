using Identity.V2.ResourceServer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IdentityModelEventSource.ShowPII = true;
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
builder.Services.AddOpenIddict()
        .AddValidation(options =>
    {
        // Note: the validation handler uses OpenID Connect discovery
        // to retrieve the address of the introspection endpoint.
        //options.SetIssuer("https://localhost:6012/");
        
        options.SetIssuer("http://host.docker.internal:5012/");
      // options.SetIssuer("https://idenv2test.o2fitt.com/");

       // options.AddAudiences("release_v2");
       options.AddAudiences("test_resource_server");

        // Configure the validation handler to use introspection and register the client
        // credentials used when communicating with the remote introspection endpoint.
       // options.UseIntrospection()
         //   .SetClientId("release_v2")
           // .SetClientSecret("D0A32D54-4EC6-46D7-A2B7-804157443129");
       

         options.AddEncryptionKey(new SymmetricSecurityKey(
            Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));
        // Register the System.Net.Http integration.
        options.UseSystemNetHttp();
        //options.EnableTokenEntryValidation();
                
        //options.UseDataProtection();
        // Register the ASP.NET Core host.
        options.UseAspNetCore();
    });

builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultScheme = OpenIddictConstants.Schemes.Bearer;
//     options.DefaultChallengeScheme = OpenIddictConstants.Schemes.Bearer;
// });
builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                
                AuthorizationUrl = new Uri("http://localhost:5012/connect/authorize"),
                TokenUrl = new Uri("http://localhost:5012/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"api2", "api2"}
                }
            },
        },
        OpenIdConnectUrl = new Uri("http://localhost:5012/.well-known/openid-configuration")
    });
    options.OperationFilter<UnauthorizedResponsesOperationFilter>(true,"oauth2");

});

// var environment = builder.Services.BuildServiceProvider().GetRequiredService<IHostingEnvironment>();

//builder.Services.AddDataProtection()
//    //.SetApplicationName(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)
//    .PersistKeysToFileSystem(new DirectoryInfo(builder.Configuration["DataProtectionSettings:Location"]!));
// builder.Services.AddDataProtection()
//     .SetApplicationName(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)
//     .PersistKeysToFileSystem(new DirectoryInfo($@"{environment.ContentRootPath}-keys"))
//     .SetDefaultKeyLifetime(TimeSpan.MaxValue);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId("release_v2");
        c.OAuthClientSecret("D0A32D54-4EC6-46D7-A2B7-804157443129");
    });
}
app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:6012","https://identity.v2.api:6012","https://idenv2test.o2fitt.com/"));
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();