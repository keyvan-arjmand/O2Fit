using Discount.Application.Common.Exceptions;
using Discount.Domain.ValueObjects;
using Discount.IntegrationTests.Utilities;
using FluentAssertions;

namespace Discount.IntegrationTests.DiscountCodeAdmin.V1.Command;

public class CreateDiscountCommandTest : BaseTestFixture
{
    //[Test]
    //public async Task CreateDiscountCommand_InsertToDbWithAmount_ReturnSuccess()
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().NotThrowAsync();
    //}

    //[Test]
    //public async Task CreateDiscountCommand_NullCodeDiscount_ThrowValidationException()
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
    //        Code = null,
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}

    //[Test]
    //public async Task CreateDiscountCommand_EmptyCodeDiscount_ThrowValidationException()
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
    //        Code = string.Empty,
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}

    //[Test]
    //public async Task CreateDiscountCommand_MaxlengthCodeDiscount_ThrowValidationException()
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
    //        Code = StringHelper.RandomString(100),
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}

    //[Test]
    //public async Task CreateDiscountCommand_MinlengthCodeDiscount_ThrowValidationException()
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
    //        Code = StringHelper.RandomString(1),
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}

    //[Test]
    //public async Task CreateDiscountCommand_NullTranslationDiscount_ThrowValidationException()
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
    //        Translation = null,
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();

    //}

    //[Test]
    //public async Task CreateDiscountCommand_StartDateBiggerDiscount_ThrowValidationException()
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
    //        StartDate = DateTime.Now.AddDays(1),
    //        Translation = null,
    //        UserIds = null,
    //        CurrencyName = currencyName,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();

    //}
    //[Test]
    //public async Task CreateDiscountCommand_StartDateEqualsEndDateDiscount_ThrowValidationException()
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}

    //[Test]
    //public async Task CreateDiscountCommand_NullCurrencyIDDiscount_ThrowValidationException()
    //{
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
    //        CurrencyName = null,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task CreateDiscountCommand_EmptyCurrencyIDDiscount_ThrowValidationException()
    //{
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
    //        CurrencyName = null,
    //        UsableCount = 20
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task CreateDiscountCommand_EmptyAmountDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountCommand()
    //    {
    //        Amount = new double(),
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task CreateDiscountCommand_ZeroAmountDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountCommand()
    //    {
    //        Amount = 0,
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}

}


