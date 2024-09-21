using Microsoft.Extensions.Configuration;
using Mongo.Migration.Documents;
using Mongo.Migration.Startup;
using Mongo.Migration.Startup.DotNetCore;
using MongoDB.Driver;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebApiServices(builder.Configuration);

//migration mongo
var _client = new MongoClient(builder.Configuration.GetSection("MongoSettings:ConnectionString").Value);

builder.Services.AddSingleton<IMongoClient>(_client);

builder.Services.AddMigration(new MongoMigrationSettings
{
    ConnectionString = builder.Configuration["MongoSettings:ConnectionString"],
    Database = builder.Configuration["MongoSettings:DatabaseName"]
    // DatabaseMigrationVersion = new DocumentVersion(1, 0, 0) // Optional
});
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.Services.GetRequiredService<IConfiguration>().InitializeConfigurations();
MapperExtensions.Configure(app.Services.GetService<IMapper>()!);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
        c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
        c.OAuthClientId("auth_server9744F0F8-ABF8-4512-BA6F-D1E88825BA4C");
        c.OAuthClientSecret("5F128CDE-45F3-4261-A452-B443702DE6D7");
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseHttpsRedirection();
app.UseCors(
    b => b.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["IdentitySettings:IdentityUrl"]!));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();