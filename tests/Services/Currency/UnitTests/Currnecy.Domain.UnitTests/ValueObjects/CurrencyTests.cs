using Currency.Domain.Exceptions.Currency;
using Currency.Domain.ValueObjects;
using Currency.IntegrationTests.Utilities;
using FluentAssertions;

namespace Currnecy.Domain.UnitTests.ValueObjects;

public class CurrencyTests
{
    [Test]
    public async Task CreateCurrency_CreateNewObject_CreateSuccessfully()
    {
        var code = StringHelper.RandomString(12);
        var currency = new Currency.Domain.Aggregates.CurrencyAggregate.Currency
        {
            CurrencyCode = new CurrencyCode(code),
        };
        currency.CurrencyCode.Name.Should().Be(code);
    }
    [Test]
    public async Task CreateCurrency_NullDiscountCode_ThrowValueObjectsException()
    {
        string code = null;
        FluentActions.Invoking(() => new Currency.Domain.Aggregates.CurrencyAggregate.Currency
        {
            CurrencyName = new CurrencyCode(code),
        })
            .Should().Throw<CurrencyTypeCannotBeNullOrEmptyException>();
    }
    [Test]
    public async Task CreateCurrency_EmptyDiscountCode_ThrowValueObjectsException()
    {
        string code = string.Empty;
        FluentActions.Invoking(() => new Currency.Domain.Aggregates.CurrencyAggregate.Currency { CurrencyCode = new CurrencyCode(code) })
            .Should().Throw<CurrencyTypeCannotBeNullOrEmptyException>();
    }
    [Test]
    public async Task CreateCurrency_PersianDiscountCode_ThrowValueObjectsException()
    {
        string code = "اکسیژن2فیت";
        FluentActions.Invoking(() => new Currency.Domain.Aggregates.CurrencyAggregate.Currency { CurrencyName = new CurrencyCode(code) })
            .Should().Throw<CurrencyTypeNotValidException>();
    }
    [Test]
    public async Task CreateCurrency_MinLengthDiscountCode_ThrowValueObjectsException()
    {
        string code = StringHelper.RandomString(1);
        FluentActions.Invoking(() => new Currency.Domain.Aggregates.CurrencyAggregate.Currency { CurrencyName = new CurrencyCode(code) })
            .Should().Throw<CurrencyCodeLenghtException>();
    }
    [Test]
    public async Task CreateCurrency_MaxLengthDiscountCode_ThrowValueObjectsException()
    {
        string code = StringHelper.RandomString(100);
        FluentActions.Invoking(() => new Currency.Domain.Aggregates.CurrencyAggregate.Currency { CurrencyName = new CurrencyCode(code) })
            .Should().Throw<CurrencyCodeLenghtException>();
    }
}