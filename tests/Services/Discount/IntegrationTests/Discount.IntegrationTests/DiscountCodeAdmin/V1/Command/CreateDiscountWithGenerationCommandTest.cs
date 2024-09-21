using Discount.Application.Common.Exceptions;
using Discount.Domain.ValueObjects;
using Discount.IntegrationTests.Utilities;
using FluentAssertions;

namespace Discount.IntegrationTests.DiscountCodeAdmin.V1.Command;

public class CreateDiscountWithGenerationCommandTest : BaseTestFixture
{
    //[Test]
    //public async Task CreateDiscountWithGenerationCommand_InsertToDbWithAmount_ReturnSuccess()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountWithGenerationCommand()
    //    {
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().NotThrowAsync();
    //}


    //[Test]
    //public async Task CreateDiscountWithGenerationCommand_NullTranslationDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountWithGenerationCommand()
    //    {
    //        Amount = 2,
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
    //public async Task CreateDiscountWithGenerationCommand_StartDateBiggerDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountWithGenerationCommand()
    //    {
    //        Amount = 2,
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
    //public async Task CreateDiscountWithGenerationCommand_StartDateEqualsEndDateDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountWithGenerationCommand()
    //    {
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}

    //[Test]
    //public async Task CreateDiscountWithGenerationCommand_NullCurrencyIDDiscount_ThrowValidationException()
    //{
    //    var command = new CreateDiscountWithGenerationCommand()
    //    {
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task CreateDiscountWithGenerationCommand_EmptyCurrencyIDDiscount_ThrowValidationException()
    //{
    //    var command = new CreateDiscountWithGenerationCommand()
    //    {
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task CreateDiscountWithGenerationCommand_EmptyAmountDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountWithGenerationCommand()
    //    {
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task CreateDiscountWithGenerationCommand_ZeroAmountDiscount_ThrowValidationException()
    //{
    //    string currencyName = StringHelper.RandomString(8);
    //    var commandCurrency = new CreateCurrencyCommand()
    //    {
    //        Name = currencyName
    //    };
    //    var currency = await Testing.SendAsync(commandCurrency);
    //    var command = new CreateDiscountWithGenerationCommand()
    //    {
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}
}