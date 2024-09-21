using Identity.V2.Grpc.Permission;
using Identity.V2.Grpc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

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
builder.Services.AddGrpc();

var app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();