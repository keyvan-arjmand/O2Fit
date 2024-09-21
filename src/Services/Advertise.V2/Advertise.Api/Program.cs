var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebApiServices(builder.Configuration);

var app = builder.Build();
MapperExtensions.Configure(app.Services.GetService<IMapper>()!);

// Configure the HTTP request pipeline.
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
        app.UseHttpsRedirection();
    }
});
app.UseCors(
    b => b.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["IdentitySettings:IdentityUrl"]!));

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();