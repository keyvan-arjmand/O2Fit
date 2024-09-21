using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebApiServices(builder.Configuration);
// var logger = new LoggerConfiguration()
//     .ReadFrom.Configuration(builder.Configuration)
//     .Enrich.FromLogContext()
//     .CreateLogger();
// builder.Logging.ClearProviders();
// builder.Logging.AddSerilog(logger);
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
        c.OAuthClientId("auth_server9744F0F8-ABF8-4512-BA6F-D1E88825BA4C");
        c.OAuthClientSecret("5F128CDE-45F3-4261-A452-B443702DE6D7");
    });
}
else
{
    app.UseHttpsRedirection();
}
app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["IdentitySettings:IdentityUrl"]!));

app.UseHealthChecks("/health");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }