
namespace Nutritionist.Command.IntegrationTests.Commands;

public class DeleteTodoListCommandTests
{
    [Test]
    public async Task TodoListDeleteConsumer_ConsumeMessage_ConsumeMessageInMemory()
    {

        // await MasstransitHarnessConfiguration.Bus.Publish<TodoListDeletedEvent>(new TodoListDeletedEvent
        // {
        //     SnowFlakeId = NewId.NextGuid()
        // });
        //
        // (await MasstransitHarnessConfiguration.TestHarness.Published.Any<TodoListDeletedEvent>()
        //     .ConfigureAwait(false)).Should().BeTrue();

        await Task.Delay(2000);
    }
}