using Nutritionist.Query.IntegrationTests.Database;
using Nutritionist.Query.IntegrationTests.Utilities;

namespace Nutritionist.Query.IntegrationTests.Queries;

public class GetByIdTodoListQueryTests : BaseTestFixture
{
    [Test]
    public async Task GetByIdTodoListQuery_GetTodoListByIdFromDb_ReturnsTodoList()
    {
        // var data = SeedData.TodoLists.FirstOrDefault();
        // var result = await SendAsync(new GetByIdTodoListQuery(data.SnowFlakeId));
        //
        // data.Id.ToString().Should().Be(result.Id);
        // data.Color.Should().Be(result.Color);
        // data.Name.Should().Be(result.Name);
        // data.SnowFlakeId.Should().Be(result.SnowFlakeId);
    }

    [Test]
    public async Task TodoListCreatedConsumer_ConsumeMessage_ConsumeMessageInMemory()
    {
        // await MasstransitHarnessConfiguration.Bus.Publish<TodoListCreatedEvent>(new
        // {
        //     SnowFlakeId = NewId.NextGuid(),
        //     Name = StringHelper.RandomString(15),
        //     Color = 1
        // });
        //
        // (await MasstransitHarnessConfiguration.TestHarness.Published.Any<TodoListCreatedEvent>()
        //    .ConfigureAwait(false)).Should().BeTrue();
        //
        // await Task.Delay(2000);
        //
        // (await MasstransitHarnessConfiguration.TestHarness.Consumed.Any<TodoListCreatedEvent>())
        //     .Should().BeTrue();
    }
}