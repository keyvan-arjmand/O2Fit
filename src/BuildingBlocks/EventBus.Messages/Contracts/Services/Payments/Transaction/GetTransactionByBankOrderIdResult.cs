namespace EventBus.Messages.Contracts.Services.Payments.Transaction;

public class GetTransactionByBankOrderIdResult
{
    public string Id { get; init; }
    public string UserId { get; init; } 
    public string? PackageId { get; init; }
    public string PackageType { get; init; } 
    public int CountryId { get; init; }
    public long BankOrderId { get; init; } //sequence Value

    public double Amount { get; init; }
    public double Wage { get; init; }
    public double Discount { get; init; }
    public double FinalAmount { get; init; }

    //when amount exchange currency
    public double? FinalAmountExchange { get; init; }
    public double? ExchangeRate { get; init; } //rate exchange
    public string? ExchangeCurrencyId { get; init; }
    public string? ExchangeCurrencyCode { get; init; } 

    public string DiscountCode { get; init; }
    public string DiscountType { get; init; }
    public string CurrencyId { get; init; } 
    public string CurrencyName { get; init; } 
    public string FinalState { get; init; } //enum
    public string Bank { get; init; }  //bank enum
    public string PaymentFor { get; init; } //paymentType Enum
    public string UserType { get; init; } //UserType Enum
    public DateTime DateTime { get; init; }

    public string? ResNum { get; init; }
    public string? RefNum { get; init; }
    public string? State { get; init; }
    public string? StateCode { get; init; }
    public string? CId { get; init; }
    public string? TraceNo { get; init; }
    public string? SecurePan { get; init; }
    public string? Authority { get; init; }
    public string? SaleReferenceId { get; init; }
}