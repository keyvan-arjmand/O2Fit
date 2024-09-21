using Serilog;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebApiServices(builder.Configuration);
 await builder.Services.AddOpenIdDictServicesAsync(builder.Configuration).ConfigureAwait(false);

if (!builder.Environment.IsDevelopment())
{
    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
}
var app = builder.Build();
// Configure the HTTP request pipeline.
app.Services.GetRequiredService<IConfiguration>().InitializeConfigurations();
MapperExtensions.Configure(app.Services.GetService<IMapper>()!);

//app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
        c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
        if (app.Environment.IsDevelopment())
        {
            c.OAuthClientId("auth_server9744F0F8-ABF8-4512-BA6F-D1E88825BA4C");
            c.OAuthClientSecret("5F128CDE-45F3-4261-A452-B443702DE6D7");
        }
        else
        {
            c.OAuthClientId("farzam");
            c.OAuthClientSecret("farzam123");    
        }
    });
//}

app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("https://localhost:6022", "https://localhost:6018", "https://localhost:6019", "http://localhost:5019",
        "http://localhost:5017", "http://localhost:5014" , "http://localhost:5012/", "https://localhost:6012/",
        "http://localhost:5020","https://localhost:6020", "http://localhost:5021","https://workout1.o2fitt.com/"
        ,"http://localhost:5030", "https://localhost:6030", "http://localhost:5031", "https://localhost:6031", "https://localhost:6014","http://localhost:5029","http://localhost:5032",
        "https://currencytest.o2fitt.com","https://discounttest.o2fitt.com", "https://orderv2test.o2fitt.com", "https://paymenttest.o2fitt.com", "https://banktest.o2fitt.com",
        "https://wallettest.o2fitt.com", "https://food1.o2fitt.com", "https://workouttest.o2fitt.com", "http://localhost:5010"));
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}



app.UseHealthChecks("/health");
app.UseAuthentication();
app.UseAuthorization();

//app.UseRateLimiter();

app.MapControllers();
app.Run();