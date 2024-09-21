using Discount.Domain.ValueObjects;
using Discount.IntegrationTests.Utilities;
using FluentAssertions;

namespace Discount.IntegrationTests.DiscountCodeNutritionist.V1.Query;

public class GetByIdDiscountCommandTest:BaseTestFixture
{
    //[Test]
    //public async Task GetByIdTodoListQuery_GetTodoListByIdFromDb_ReturnsTodoList()
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
    //        EndDateTime = DateTime.UtcNow.AddDays(1),
    //        IsActive = true,
    //        StartDate = DateTime.UtcNow,
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
    //    var result = await Testing.SendAsync(new GetByIdDiscountQuery(discount));
    //    command.Amount.Should().Be(result.Amount);
    //    command.Code.Should().Be(result.Code);
    //    command.Translation.English.Should().Be(result.Translation.English);
    //    command.Translation.Arabic.Should().Be(result.Translation.Arabic);
    //    command.Translation.Persian.Should().Be(result.Translation.Persian);
    //    //command.StartDate.Should().Be(result.StartDate);
    //    //command.EndDateTime.Should().Be(result.EndDateTime);
    //    command.UsableCount.Should().Be(result.UsableCount);
     
    //}
}