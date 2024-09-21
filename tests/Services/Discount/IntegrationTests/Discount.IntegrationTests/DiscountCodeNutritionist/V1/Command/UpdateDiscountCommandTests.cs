using Discount.Application.Common.Exceptions;
using Discount.Domain.ValueObjects;
using Discount.IntegrationTests.Utilities;
using FluentAssertions;

namespace Discount.IntegrationTests.DiscountCodeNutritionist.V1.Command;

public class UpdateDiscountCommandTests : BaseTestFixture
{
    //[Test]
    //public async Task UpdateDiscountCommand_InsertToDbWithAmount_ReturnSuccess()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountCommand()
    //    {
    //        Amount = 2,
    //        Code = StringHelper.RandomString(10),
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    var discount = await Testing.SendAsync(command);
    //    var commandUpdate = new UpdateDiscountCommand()
    //    {
    //        Id = discount,
    //        Amount = 2,
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().NotThrowAsync();
    //}
    //[Test]
    //public async Task UpdateDiscountCommand_NullDiscountId_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);

    //    var commandUpdate = new UpdateDiscountCommand()
    //    {
    //        Id = null,
    //        Amount = 2,
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task UpdateDiscountCommand_EmptyDiscountId_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var commandUpdate = new UpdateDiscountCommand()
    //    {
    //        Id = string.Empty,
    //        Amount = 2,
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();
    //}
 
    //[Test]
    //public async Task UpdateDiscountCommand_NullTranslationDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountCommand()
    //    {
    //        Amount = 2,
    //        Code = StringHelper.RandomString(10),
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    var discount = await Testing.SendAsync(command);
    //    var commandUpdate = new UpdateDiscountCommand()
    //    {
    //        Id = discount,
    //        Amount = 2,
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = null,
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();

    //}

    //[Test]
    //public async Task UpdateDiscountCommand_StartDateBiggerDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountCommand()
    //    {
    //        Amount = 2,
    //        Code = StringHelper.RandomString(10),
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    var discount = await Testing.SendAsync(command);
    //    var commandUpdate = new UpdateDiscountCommand()
    //    {
    //        Id = discount,
    //        Amount = 2,
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now.AddDays(1),
    //        Translation = null,
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();

    //}
    //[Test]
    //public async Task UpdateDiscountCommand_StartDateEqualsEndDateDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountCommand()
    //    {
    //        Amount = 2,
    //        Code = StringHelper.RandomString(10),
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    var discount = await Testing.SendAsync(command);
    //    var commandUpdate = new UpdateDiscountCommand()
    //    {
    //        Id = discount,
    //        Amount = 2,
    //        EndDateTime = DateTime.Now,
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();
    //}

    //[Test]
    //public async Task UpdateDiscountCommand_NullCurrencyIDDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountCommand()
    //    {
    //        Amount = 2,
    //        Code = StringHelper.RandomString(10),
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    var discount = await Testing.SendAsync(command);
    //    var commandUpdate = new UpdateDiscountCommand()
    //    {
    //        Id = discount,
    //        Amount = 2,
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = null,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task UpdateDiscountCommand_EmptyCurrencyIDDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountCommand()
    //    {
    //        Amount = 2,
    //        Code = StringHelper.RandomString(10),
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    var discount = await Testing.SendAsync(command);
    //    var commandUpdate = new UpdateDiscountCommand()
    //    {
    //        Id = discount,
    //        Amount = 2,
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = null,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task UpdateDiscountCommand_EmptyAmountDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountCommand()
    //    {
    //        Amount = 2,
    //        Code = StringHelper.RandomString(10),
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    var discount = await Testing.SendAsync(command);
    //    var commandUpdate = new UpdateDiscountCommand()
    //    {
    //        Id = discount,
    //        Amount = new double(),
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task UpdateDiscountCommand_ZeroAmountDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountCommand()
    //    {
    //        Amount = 2,
    //        Code = StringHelper.RandomString(10),
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    var discount = await Testing.SendAsync(command);
    //    var commandUpdate = new UpdateDiscountCommand()
    //    {
    //        Id = discount,
    //        Amount = 0,
    //        EndDateTime = DateTime.Now.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.Now,
    //        Translation = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10)
    //        },
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandUpdate)).Should().ThrowAsync<ValidationException>();
    //}
}