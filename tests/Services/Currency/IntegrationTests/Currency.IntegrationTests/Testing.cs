using static Currency.IntegrationTests.TestContainers;

namespace Currency.IntegrationTests;

[SetUpFixture]
public partial class Testing
{
    private static WebApplicationFactory<Program> _factory = null!;
    public static IConfiguration Configuration { get; set; } = default!;

    private static IServiceScopeFactory _scopeFactory = null!;
    [OneTimeSetUp]
    public async Task RunBeforeAnyTests()
    {
        await InitializeAsync().ConfigureAwait(false);
        _factory = new CustomWebApplicationFactory();
        Configuration = _factory.Services.GetRequiredService<IConfiguration>();
        await MasstransitHarnessConfiguration.UseHarnessAsync().ConfigureAwait(false);
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static IUnitOfWork CreateUnitOfWork()
    {
        using var scope = _scopeFactory.CreateScope();

        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        return uow;
    }

    [OneTimeTearDown]
    public Task RunAfterAnyTestsAsync()
    {
        return Task.WhenAll(DisposeAsync());
    }
}