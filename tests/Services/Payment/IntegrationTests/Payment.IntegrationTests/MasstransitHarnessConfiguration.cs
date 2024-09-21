
namespace Payment.IntegrationTests;

public class MasstransitHarnessConfiguration
{
    //public static InMemoryTestHarness TestHarness { get; set; } = default!;
    //public static IBus TestHarnessBus { get; set; } = default!;
    //public static async Task UseHarnessAsync()
    //{
    //    //EndpointConvention.Map<GetCurrencyByNameRequest>(new Uri("queue:get-currency-by-name-request"));

    //    //  EndpointConvention.Map<TodoListCreatedEvent>(new Uri("queue:todo_list_created"));
    //    await using var provider = new ServiceCollection()
    //            .AddScoped<IUnitOfWork, UnitOfWork>()
              

    //            .AddMassTransitInMemoryTestHarness(x =>
    //                    {
    //                        x.SetKebabCaseEndpointNameFormatter();
    //                        x.AddConsumer<GetCurrencyByNameConsumer>();
    //                        //                      .Endpoint(x=>x.Name = "get-currency-by-name-request");

    //                        x.AddConsumerTestHarness<GetCurrencyByNameConsumer>();
    //                        //x.AddConsumers(typeof(GetCurrencyByNameConsumer));
    //                        // x.AddConsumer<TodoListDeletedConsumer>();
    //                        // x.AddConsumer<TodoListUpdatedConsumer>();
    //                        //x.SetTestTimeouts(testInactivityTimeout: TimeSpan.FromMinutes(3));
    //                    })

    //            .AddGenericRequestClient()
    //            .BuildServiceProvider(true);

    //    TestHarness = provider.GetRequiredService<InMemoryTestHarness>();
    //    await TestHarness.Start().ConfigureAwait(false); 
    //    //TestHarness.TestInactivityTimeout = TimeSpan.FromMinutes(2);
    //    //TestHarness.TestTimeout = TimeSpan.FromMinutes(2);
    //    TestHarnessBus = provider.GetRequiredService<IBus>();

    //}


}
