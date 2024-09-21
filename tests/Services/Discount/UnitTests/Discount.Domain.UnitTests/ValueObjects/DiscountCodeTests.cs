using Discount.Domain.Exceptions.Discount;
using Discount.Domain.ValueObjects;
using Discount.IntegrationTests.Utilities;
using FluentAssertions;

namespace Discount.Domain.UnitTests.ValueObjects;

public class DiscountCodeTests
{
    [Test]
    public async Task CreateDiscountCode_CreateNewObject_CreateSuccessfully()
    {
        var code = StringHelper.RandomString(6);
        var discountCode = new DiscountCode(code);
        discountCode.Code.Should().Be(code);
    }
    [Test]
    public async Task CreateDiscountCode_NullDiscountCode_ThrowValueObjectsException()
    {
        string code = null;
        FluentActions.Invoking(() => new DiscountCode(code))
            .Should().Throw<DiscountCodeTypeCannotBeNullOrEmptyException>();
    }
    [Test]
    public async Task CreateDiscountCode_EmptyDiscountCode_ThrowValueObjectsException()
    {
        string code = string.Empty;
        FluentActions.Invoking(() => new DiscountCode(code))
            .Should().Throw<DiscountCodeTypeCannotBeNullOrEmptyException>();
    }
    [Test]
    public async Task CreateDiscountCode_PersianDiscountCode_ThrowValueObjectsException()
    {
        string code = "تستtest";
        FluentActions.Invoking(() => new DiscountCode(code))
            .Should().Throw<DiscountCodeNotValidException>();
    }
    [Test]
    public async Task CreateDiscountCode_MinLengthDiscountCode_ThrowValueObjectsException()
    {
        string code = StringHelper.RandomString(1);
        FluentActions.Invoking(() => new DiscountCode(code))
            .Should().Throw<DiscountCodeMinLengthIs4CharacterException>();
    }
    [Test]
    public async Task CreateDiscountCode_MaxLengthDiscountCode_ThrowValueObjectsException()
    {
        string code = StringHelper.RandomString(100);
        FluentActions.Invoking(() => new DiscountCode(code))
            .Should().Throw<DiscountCodeTypeMaxlengthIs10CharacterException>();
    }
}