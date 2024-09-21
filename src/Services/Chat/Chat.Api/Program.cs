var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IdentityModelEventSource.ShowPII = true;

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebApiServices(builder.Configuration);

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
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
        c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
        c.OAuthClientId("farzam");
        c.OAuthClientSecret("farzam123");   
    });
    app.UseHttpsRedirection();
}

//app.UseCors(
//    b => b.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["IdentitySettings:IdentityUrl"]!));
app.UseCors("CorsPolicy");
app.UseRouting();

app.UseHealthChecks("/health");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("chat-hub");
app.MapHub<TestHub>("test-hub");
app.Run();