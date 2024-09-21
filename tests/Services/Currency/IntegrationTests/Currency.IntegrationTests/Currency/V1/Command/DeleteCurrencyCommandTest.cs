using Currency.Application.Common.Exceptions;
using Currency.Application.Currencies.V1.Commands.CreateCurrency;
using Currency.Application.Currencies.V1.Commands.DeleteCurrency;
using Currency.IntegrationTests.Utilities;
using FluentAssertions;

namespace Currency.IntegrationTests.Currency.V1.Command;

public class DeleteCurrencyCommandTest : BaseTestFixture
{
    //[Test]
    //public async Task DeleteCurrencyCommand_DeleteInDb_ReturnSuccess()
    //{
    //    var command = new CreateCurrencyCommand()
    //    {
    //        Name = StringHelper.RandomString(8)
    //    };
    //    var currency = await Testing.SendAsync(command);

    //    var deleteCommand = new DeleteCurrencyCommand()
    //    {
    //        Id = currency
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(deleteCommand)).Should().NotThrowAsync();
    //}
    //[Test]
    //public async Task DeleteCurrencyCommand_NullCurrencyId_ThrowValidationException()
    //{
    //    var deleteCommand = new DeleteCurrencyCommand()
    //    {
    //        Id = null
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(deleteCommand)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task DeleteCurrencyCommand_EmptyCurrencyId_ThrowValidationException()
    //{
    //    var deleteCommand = new DeleteCurrencyCommand()
    //    {
    //        Id = string.Empty
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(deleteCommand)).Should().ThrowAsync<ValidationException>();
    //}
}