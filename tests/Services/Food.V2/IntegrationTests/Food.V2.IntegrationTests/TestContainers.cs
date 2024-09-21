using DotNet.Testcontainers.Builders;
using Testcontainers.EventStoreDb;
using Testcontainers.MongoDb;
using Testcontainers.RabbitMq;

namespace Food.V2.IntegrationTests;

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
            .WithPortBinding(29003, 27017)
            .Build();


        RabbitMqContainer = new RabbitMqBuilder().WithImage("masstransit/rabbitmq")
            .WithPortBinding(1127, 5672)
            .WithPortBinding(2347, 15672)
            .WithUsername("guest")
            .WithPassword("guest")
            .Build();

        EventStoreDbContainer = new EventStoreDbBuilder().WithImage("eventstore/eventstore:21.10.0-buster-slim")
            .WithPortBinding(3118, 1113)
            .WithPortBinding(4118, 2113)
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