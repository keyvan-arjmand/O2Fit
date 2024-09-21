var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddGrpcServices(builder.Configuration);
builder.Services.AddGrpc();
var app = builder.Build();
app.Services.GetRequiredService<IConfiguration>().InitializeConfigurations();
// Configure the HTTP request pipeline.

app.MapGrpcService<ExchangeCurrencyService>();
app.MapGet("/",() =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();