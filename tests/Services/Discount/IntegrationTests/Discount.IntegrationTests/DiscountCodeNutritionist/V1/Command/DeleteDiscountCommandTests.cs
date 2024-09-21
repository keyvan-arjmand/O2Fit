using Discount.Domain.ValueObjects;
using Discount.IntegrationTests.Utilities;
using FluentAssertions;

namespace Discount.IntegrationTests.DiscountCodeNutritionist.V1.Command;

public class DeleteDiscountCommandTests : BaseTestFixture
{
    //[Test]
    //public async Task DeleteDiscountCommand_DeleteInDb_ReturnSuccess()
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
    //    var deleteCommand = new DeleteDiscountCommand
    //    {
    //        Id = discount
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(deleteCommand)).Should().NotThrowAsync();
    //}
    //[Test]
    //public async Task DeleteDiscountCommand_NullDiscountId_ThrowValidationException()
    //{
    //    var deleteCommand = new DeleteDiscountCommand
    //    {
    //        Id = null
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(deleteCommand)).Should().ThrowAsync<Application.Common.Exceptions.ValidationException>();
    //}
    //[Test]
    //public async Task DeleteDiscountCommand_EmptyDiscountId_ThrowValidationException()
    //{
    //    var deleteCommand = new DeleteDiscountCommand
    //    {
    //        Id = string.Empty
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(deleteCommand)).Should().ThrowAsync<Application.Common.Exceptions.ValidationException>();
    //}
}