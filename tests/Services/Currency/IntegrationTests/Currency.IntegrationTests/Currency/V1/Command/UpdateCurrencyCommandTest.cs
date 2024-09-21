using Currency.Application.Common.Exceptions;
using Currency.Application.Currencies.V1.Commands.CreateCurrency;
using Currency.Application.Currencies.V1.Commands.UpdateCurrency;
using Currency.IntegrationTests.Utilities;
using FluentAssertions;

namespace Currency.IntegrationTests.Currency.V1.Command;

public class UpdateCurrencyCommandTest:BaseTestFixture
{
    //[Test]
    //public async Task UpdateCurrencyCommand_InsertTo_ReturnSuccess()
    //{
    //    var commandCreate = new CreateCurrencyCommand
    //    {
    //        Name = StringHelper.RandomString(8)
    //    };
    //    var currency = await Testing.SendAsync(commandCreate);
    //    var commandUpdate = new UpdateCurrencyCommand
    //    {
    //        Id = currency,
    //        Name = StringHelper.RandomString(9)
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().NotThrowAsync();
    //}
    //[Test]
    //public async Task UpdateCurrencyCommand_NullCurrencyId_ReturnSuccess()
    //{
   
    //    var commandUpdate = new UpdateCurrencyCommand
    //    {
    //        Id = null,
    //        Name = StringHelper.RandomString(9)
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task UpdateCurrencyCommand_EmptyCurrencyId_ReturnSuccess()
    //{
      
    //    var commandUpdate = new UpdateCurrencyCommand
    //    {
    //        Id = string.Empty,
    //        Name = StringHelper.RandomString(9)
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task UpdateCurrencyCommand_EmptyNameCurrency_ThrowValidationException()
    //{
    //    var commandCreate = new CreateCurrencyCommand
    //    {
    //        Name = StringHelper.RandomString(8)
    //    };
    //    var currency = await Testing.SendAsync(commandCreate);
    //    var commandUpdate = new UpdateCurrencyCommand
    //    {
    //        Id = currency,
    //        Name = string.Empty
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task UpdateCurrencyCommand_NullNameCurrency_ThrowValidationException()
    //{
    //    var commandCreate = new CreateCurrencyCommand
    //    {
    //        Name = StringHelper.RandomString(8)
    //    };
    //    var currency = await Testing.SendAsync(commandCreate);
    //    var commandUpdate = new UpdateCurrencyCommand
    //    {
    //        Id = currency,
    //        Name = null
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task UpdateCurrencyCommand_MinLengthNameCurrency_ThrowValidationException()
    //{
    //    var commandCreate = new CreateCurrencyCommand
    //    {
    //        Name = StringHelper.RandomString(8)
    //    };
    //    var currency = await Testing.SendAsync(commandCreate);
    //    var commandUpdate = new UpdateCurrencyCommand
    //    {
    //        Id = currency,
    //        Name = StringHelper.RandomString(1)
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task UpdateCurrencyCommand_MaxLengthNameCurrency_ThrowValidationException()
    //{
    //    var commandCreate = new CreateCurrencyCommand
    //    {
    //        Name = StringHelper.RandomString(8)
    //    };
    //    var currency = await Testing.SendAsync(commandCreate);
    //    var commandUpdate = new UpdateCurrencyCommand
    //    {
    //        Id = currency,
    //        Name = StringHelper.RandomString(400)
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();
    //}
}