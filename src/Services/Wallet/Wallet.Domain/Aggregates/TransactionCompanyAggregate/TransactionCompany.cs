using Wallet.Domain.Common;

namespace Wallet.Domain.Aggregates.TransactionCompanyAggregate;

public class TransactionCompany : AggregateRoot
{
    public ObjectId UserId { get; set; }
    public ObjectId? NutritionistId { get; set; } //when order Accepted
    public ObjectId? OrderId { get; set; } //when order Accepted or accept
    public ObjectId WalletId { get; set; }
    
    public ObjectId WalletTransactionId { get; set; }
    public ObjectId PaymentTransactionId { get; set; }
    public ObjectId? PackageId { get; set; }
    public string? PackageType { get; set; } = string.Empty;
    public ObjectId CurrencyId { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public DateTime DateTime { get; set; }
    public double Amount { get; set; } //مبلغ کل
    public double FinalAmount { get; set; }
    //when amount exchange currency
    public double? FinalAmountExchange { get; set; }
    public double? ExchangeRate { get; set; } //rate exchange
    public ObjectId? ExchangeCurrencyId { get; set; }
    public string? ExchangeCurrencyCode { get; set; } = string.Empty;
    //commission - Payment For Package 
    public double? NetIncome { get; set; } //درآمد خالص-ارزش افزوده
    public double? Income { get; set; } //درآمد ناخالص+ارزش افزوده وکمیسیون کمپانی
    public double? ValueAdded { get; set; } //مالیات
    public double? DebtToTheNutritionist { get; set; } //بدهی به متخصص

    //Package
    public double Wage { get; set; }
    public double Discount { get; set; }
    public string? DiscountCode { get; set; }
    //type
    public string DiscountType { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty; //enum UserType
    public string PaymentFor { get; set; } = string.Empty; //enum PaymentType
    public string TransactionType { get; set; } = string.Empty; //enum TransactionType
    public string Bank { get; set; } = string.Empty;//enum bank
    //bank params
    public string? SaleReferenceId { get; set; }
    public string? TraceNo { get; set; }
    public long BankOrderId { get; set; }

}