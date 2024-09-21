using Currency.Application.Common.Exceptions;
using Currency.Application.Consumers.Currencies;
using Currency.Application.Currencies.V1.Commands.CreateCurrency;
using Currency.IntegrationTests.Utilities;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using FluentAssertions;

namespace Currency.IntegrationTests.Currency.V1.Command;

public class CreateCurrencyCommandTest : BaseTestFixture
{
    //[Test]
    //public async Task CreateCurrencyCommand_InsertTo_ReturnSuccess()
    //{
    //    var command = new CreateCurrencyCommand()
    //    {
    //        Name = StringHelper.RandomString(8)
    //    };
    //    var currency = await Testing.SendAsync(command);
    //    currency.Should().NotBeNull();
    //    currency.Should().NotBeEmpty();
    //    currency.Should().BeOfType<string>();
    //}
    //[Test]
    //public async Task CreateCurrencyCommand_BusPublish_PublishConsumer()
    //{
    //    await MasstransitHarnessConfiguration.Bus.Publish<GetCurrencyByNameConsumer>(new GetCurrencyByName
    //    {
    //        Name = "test"
    //    });
       
    //    var published = await MasstransitHarnessConfiguration.TestHarness.Published.Any<GetCurrencyByName>()
    //        .ConfigureAwait(false);
    //    var consumer = await MasstransitHarnessConfiguration.TestHarness.Consumed.Any<GetCurrencyByNameConsumer>()
    //        .ConfigureAwait(false);
    //    published.Should().BeTrue();
    //    consumer.Should().BeTrue();
    //}

    //[Test]
    //public async Task CreateCurrencyCommand_EmptyNameCurrency_ThrowValidationException()
    //{
    //    var command = new CreateCurrencyCommand()
    //    {
    //        Name = string.Empty
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}

    //[Test]
    //public async Task CreateCurrencyCommand_DuplicateCurrencyName_ThrowAppException()
    //{
    //    var commandFirst = new CreateCurrencyCommand()
    //    {
    //        Name = "test1"
    //    };
    //    await FluentActions.Invoking(()=> Testing.SendAsync(commandFirst)).Should().NotThrowAsync();
    //    var commandSec = new CreateCurrencyCommand()
    //    {
    //        Name = "test1"
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandSec)).Should().ThrowAsync<AppException>();
    //}

    //[Test]
    //public async Task CreateCurrencyCommand_NullNameCurrency_ThrowValidationException()
    //{
    //    var command = new CreateCurrencyCommand()
    //    {
    //        Name = null
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}

    //[Test]
    //public async Task CreateCurrencyCommand_MinLengthNameCurrency_ThrowValidationException()
    //{
    //    var command = new CreateCurrencyCommand()
    //    {
    //        Name = StringHelper.RandomString(1)
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}

    //[Test]
    //public async Task CreateCurrencyCommand_MaxLengthNameCurrency_ThrowValidationException()
    //{
    //    var command = new CreateCurrencyCommand()
    //    {
    //        Name = StringHelper.RandomString(500)
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}
}