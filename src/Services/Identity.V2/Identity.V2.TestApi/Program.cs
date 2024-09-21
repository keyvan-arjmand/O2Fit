using System.Security.Claims;
using Identity.V2.TestApi.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true;
// Register the OpenIddict validation components.
builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {
        // Note: the validation handler uses OpenID Connect discovery
        // to retrieve the address of the introspection endpoint.
        //options.SetIssuer("https://localhost:6012/");
        options.SetIssuer("https://identity.v2.api:6012/");

        options.AddAudiences("test_api_server");

        // Configure the validation handler to use introspection and register the client
        // credentials used when communicating with the remote introspection endpoint.
        options.UseIntrospection()
            .SetClientId("test_api_server")
            .SetClientSecret("846B62D0-DEF9-4215-A99D-86E6B8DAB342");
       

       // options.AddEncryptionKey(new SymmetricSecurityKey(
       //     Convert.FromBase64String("IbaAsWVLFTPpehI6Ej38M0Qu4ZsAnlbPCsOJNprDrp8=")));
        // Register the System.Net.Http integration.
        options.UseSystemNetHttp();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();
    });
builder.Services.AddCors();
builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "oauth2");
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                
                AuthorizationUrl = new Uri("https://localhost:6012/connect/authorize"),
                TokenUrl = new Uri("https://localhost:6012/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "api1", "resource server scope" }
                }
            },
        },
        OpenIdConnectUrl = new Uri("https://localhost:6012/.well-known/openid-configuration")
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            },
            Array.Empty<string>()
        }
    });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.OAuthClientId("test_api_server");
    c.OAuthClientSecret("846B62D0-DEF9-4215-A99D-86E6B8DAB342");
});
app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:6012"));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api", [Authorize] (ClaimsPrincipal user) => $"{user.Identity!.Name} is allowed to access Api1.");

app.Run();