using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Nutritionist.Command.IntegrationTests;

[SetUpFixture]
public partial class Testing
{
    private static WebApplicationFactory<Program> _factory = null!;
    public static IConfiguration Configuration { get; set; } = default!;

    private static IServiceScopeFactory _scopeFactory = null!;
    [OneTimeSetUp]
    public async Task RunBeforeAnyTests()
    {
        await TestContainers.InitializeAsync().ConfigureAwait(false);
        _factory = new CustomWebApplicationFactory();
        // await MasstransitHarnessConfiguration.UseHarnessAsync().ConfigureAwait(false);
        Configuration = _factory.Services.GetRequiredService<IConfiguration>();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    // public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    // {
    //     using var scope = _scopeFactory.CreateScope();
    //
    //     var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
    //
    //     return await mediator.Send(request);
    // }
    //
    // public static async Task SendAsync(IBaseRequest request)
    // {
    //     using var scope = _scopeFactory.CreateScope();
    //
    //     var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
    //
    //     await mediator.Send(request);
    // }
    //

    [OneTimeTearDown]
    public Task RunAfterAnyTestsAsync()
    {
        return TestContainers.DisposeAsync();
    }
}