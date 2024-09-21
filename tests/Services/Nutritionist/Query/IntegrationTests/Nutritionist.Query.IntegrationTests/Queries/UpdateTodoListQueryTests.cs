using Nutritionist.Query.IntegrationTests.Utilities;

namespace Nutritionist.Query.IntegrationTests.Queries;

public class UpdateTodoListQueryTests
{
    [Test]
    public async Task TodoListUpdateConsumer_ConsumeMessage_ConsumeMessageInMemory()
    {

        // await MasstransitHarnessConfiguration.Bus.Publish<TodoListUpdatedEvent>(new TodoListUpdatedEvent
        // {
        //   Name = StringHelper.RandomString(10),
        //   Color = 1,
        //   SnowFlakeId = NewId.NextGuid()
        // });
        //
        // (await MasstransitHarnessConfiguration.TestHarness.Published.Any<TodoListUpdatedEvent>()
        //     .ConfigureAwait(false)).Should().BeTrue();
        //
        // await Task.Delay(2000);
        //
        // (await MasstransitHarnessConfiguration.TestHarness.Consumed.Any<TodoListUpdatedEvent>())
        //     .Should().BeTrue();
    }
}