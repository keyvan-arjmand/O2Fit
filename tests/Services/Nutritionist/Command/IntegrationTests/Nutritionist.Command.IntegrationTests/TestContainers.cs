namespace Nutritionist.Command.IntegrationTests;

[SetUpFixture]
public class TestContainers
{
    public static MongoDbContainer MongoDbCommandContainer = null!;
    public static RabbitMqContainer RabbitMqContainer = null!;
    public static EventStoreDbContainer EventStoreDbContainer = null!;

    [OneTimeSetUp]
    public void SetUpTestContainers()
    {
        MongoDbCommandContainer = new MongoDbBuilder().WithImage("mongo")
            .WithUsername("admin")
            .WithPassword("StrongPassword")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(x => x.ForPort(27017)))
            .WithCleanUp(true)
            .WithPortBinding(28000, 27017)
            .Build();


        RabbitMqContainer = new RabbitMqBuilder().WithImage("masstransit/rabbitmq")
            .WithPortBinding(1123, 5672)
            .WithPortBinding(2345, 15672)
            .WithUsername("guest")
            .WithPassword("guest")
            .Build();

        EventStoreDbContainer = new EventStoreDbBuilder().WithImage("eventstore/eventstore:21.10.0-buster-slim")
            .WithPortBinding(3113, 1113)
            .WithPortBinding(4113, 2113)
            .WithCleanUp(true)
            .Build();

    }

    public static Task InitializeAsync()
    {
        return Task.WhenAll(EventStoreDbContainer.StartAsync(),
            MongoDbCommandContainer.StartAsync(), RabbitMqContainer.StartAsync());
    }

    public static async Task DisposeAsync()
    {
        await RabbitMqContainer.DisposeAsync().ConfigureAwait(false);
        await MongoDbCommandContainer.DisposeAsync().ConfigureAwait(false);
        await EventStoreDbContainer.DisposeAsync().ConfigureAwait(false);
    }
}