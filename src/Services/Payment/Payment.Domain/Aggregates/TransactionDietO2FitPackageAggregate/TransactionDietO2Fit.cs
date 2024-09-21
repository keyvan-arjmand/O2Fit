using Payment.Domain.Common;

namespace Payment.Domain.Aggregates.TransactionDietO2FitPackageAggregate;

public class TransactionDietO2Fit : AggregateRoot
{
    public ObjectId? PackageId { get; set; } //diet
    public long BankOrderId { get; set; } //sequence Value
    public double Amount { get; set; }
    public double Wage { get; set; }
    public double Discount { get; set; }
    
    public double FinalAmount { get; set; }

    //when amount exchange => wallet currency 
    public double? FinalAmountExchange { get; set; }
    public double? ExchangeRate { get; set; } //rate exchange
    public ObjectId? ExchangeCurrencyId { get; set; }
    public string? ExchangeCurrencyCode { get; set; }
    public string? DiscountCode { get; set; }
    public string? DiscountType { get; set; } //enum
    public ObjectId CurrencyId { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public string FinalState { get; set; } = string.Empty; //enum
    public string Bank { get; set; } = string.Empty; //bank enum
    public string PaymentFor { get; set; } = string.Empty; //paymentType Enum
    public string UserType { get; set; } = string.Empty; //UserType Enum
    public DateTime PaymentTime { get; set; }
    public string? ResNum { get; set; }
    public string? RefNum { get; set; }

    public string? State { get; set; }
    public string? StateCode { get; set; }
    public string? CId { get; set; }
    public string? TraceNo { get; set; }
    public string? SecurePan { get; set; }
    public string? Authority { get; set; }
    public string? SaleReferenceId { get; set; }
}