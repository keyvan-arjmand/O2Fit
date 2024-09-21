namespace Order.V2.Infrastructure.Common;

public static class MediatorExtensions
{
    private static IConfiguration? Configuration { get; set; }

    public static async Task DispatchDomainEventsAsync(this IMediator mediator, AggregateRoot aggregateRoot)
    {
        var domainEvents = aggregateRoot.DomainEvents;


        foreach (var domainEvent in domainEvents)
        {
            // await AddEventToEventStore(domainEvent).ConfigureAwait(false);
            await mediator.Publish(domainEvent).ConfigureAwait(false);
        }

        aggregateRoot.ClearDomainEvents();

    }

    public static async Task DispatchDomainEventsAsync(this IMediator mediator, IEnumerable<AggregateRoot> aggregateRoots)
    {
        foreach (var aggregateRoot in aggregateRoots)
        {
            var domainEvents = aggregateRoot.DomainEvents;

            foreach (var domainEvent in domainEvents)
            {
                // await AddEventToEventStore(domainEvent).ConfigureAwait(false);
                await mediator.Publish(domainEvent).ConfigureAwait(false);
            }

            aggregateRoot.ClearDomainEvents();

        }

    }

    public static void InitializeConfigurations(this IConfiguration configuration)
    {
        Configuration = configuration;
    }
    private static Task AddEventToEventStore(BaseEvent @event)
    {
        var serialize = JsonConvert.SerializeObject(@event);
        var settings = EventStoreClientSettings
            .Create(Configuration!["EventStoreSettings:ConnectionStrings"]);
        var client = new EventStoreClient(settings);
        var eventData = new EventData(
            Uuid.NewUuid(),
            @event.GetType().FullName,
            Encoding.UTF8.GetBytes(serialize)
        );

        return client.AppendToStreamAsync(
            "payment-stream",
            StreamState.Any,
            new List<EventData> { eventData }
        );
    }
}
