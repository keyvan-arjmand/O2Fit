
namespace Nutritionist.Command.IntegrationTests.Commands;

public class CreateTodoListCommandTests : BaseTestFixture
{
    [Test]
    public async Task CreateTodoListCommand_InsertToDb_ReturnsSnowFlakeId()
    {
        // var randomString = StringHelper.RandomString(9);
        // var snowFlakeId = await SendAsync(new CreateTodoListCommand(randomString, Color.Blue));
        // snowFlakeId.Should().NotBeEmpty();
    }

    [Test]
    public async Task CreateTodoListCommand_RaiseEvent_SendEventToInMemoryBroker()
    {
      
      //  await MasstransitHarnessConfiguration.Bus.Publish<TodoListCreatedEvent>(new
      //   {
      //       SnowFlakeId = NewId.NextGuid(),
      //       Name = StringHelper.RandomString(9),
      //       Color = (int)Color.Blue
      //   });
      //  
      // var result = await MasstransitHarnessConfiguration.TestHarness.Published.Any<TodoListCreatedEvent>()
      //     .ConfigureAwait(false);
      // result.Should().BeTrue();
    }
}