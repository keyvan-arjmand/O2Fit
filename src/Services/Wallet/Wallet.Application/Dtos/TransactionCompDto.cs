using MongoDB.Bson;

namespace Wallet.Application.Dtos;

public class TransactionCompDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string NutritionistId { get; set; } = string.Empty; //when order Accepted
    public string OrderId { get; set; } = string.Empty; //when order Accepted or accept
    public string WalletId { get; set; } = string.Empty;
    public string WalletTransactionId { get; set; } = string.Empty;
    public string PaymentTransactionId { get; set; } = string.Empty;
    public string PackageId { get; set; } = string.Empty;
    public string PackageType { get; set; } = string.Empty;
    public int DurationPackage { get; set; }

    public string CurrencyId { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public DateTime DateTime { get; set; }
    public double Amount { get; set; } //مبلغ کل
    public double FinalAmount { get; set; }
    //when amount exchange currency
    public double FinalAmountExchange { get; set; }
    public double ExchangeRate { get; set; } //rate exchange
    public string ExchangeCurrencyId { get; set; } = string.Empty;
    public string ExchangeCurrencyCode { get; set; } = string.Empty;
    //commission - Payment For Package 
    public double NetIncome { get; set; } //درآمد خالص-ارزش افزوده
    public double Income { get; set; } //درآمد ناخالص+ارزش افزوده وکمیسیون کمپانی
    public double ValueAdded { get; set; } //مالیات
    public double DebtToTheNutritionist { get; set; } //بدهی به متخصص

    //Package
    public double Wage { get; set; }
    public double Discount { get; set; }
    public string DiscountCode { get; set; } = string.Empty;
    //type
    public string DiscountType { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;//enum UserType
    public string PaymentFor { get; set; } = string.Empty; //enum PaymentType
    public string TransactionType { get; set; } = string.Empty; //enum TransactionType
    public string Bank { get; set; } = string.Empty;//enum bank
    //bank params
    public string SaleReferenceId { get; set; } = string.Empty;
    public string TraceNo { get; set; } = string.Empty;
    public long BankOrderId { get; set; }
}