using Common.Constants.Track.Migrations.TrackWater;
using Mongo.Migration.Documents;
using Mongo.Migration.Startup;
using Mongo.Migration.Startup.DotNetCore;
using MongoDB.Driver;
using Serilog;
using Track.Application.Common.Utilities;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebApiServices(builder.Configuration);
builder.Services.AddAuthenticationServices(builder.Configuration);


var app = builder.Build();

//app.UseMiddleware<ExceptionMiddleware>();

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
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
        c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
    });
    app.UseHttpsRedirection();
}


app.UseHealthChecks("/health");
app.UseStaticFiles();
app.MapControllers();

app.Run();

namespace Track.Api
{
    public partial class Program
    {
    }
}